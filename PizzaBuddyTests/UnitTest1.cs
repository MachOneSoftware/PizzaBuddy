using System;
using Xunit;
using MachOneSoftware.PizzaBuddy;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Amazon.Lambda.Core;
using Moq;
using System.Reflection;

namespace PizzaBuddyTests
{
    public class UnitTest1
    {
        [Fact]
        public void FunctionReturnsOpeningSpeechWhenLaunched()
        {
            //arrange 
            Function SUT = new Function();
            SkillRequest input = new SkillRequest();
            input.Request = new LaunchRequest();
           
            var mockContext =new  Mock<ILambdaContext>();
            mockContext.Setup(x => x.AwsRequestId).Returns("MOCK_AWS_REQUEST");

            //Act
            var response = SUT.FunctionHandler(input, mockContext.Object);
            var POT = (Alexa.NET.Response.PlainTextOutputSpeech)response.Response.OutputSpeech;
            string c = SUT.GetStaticPrivateConst<string>("LaunchMessage");
            
            //Assert
            Assert.Equal(POT.Text, c);
        }
    }


    public static class ReflectionExtensions
    {
        public static T GetStaticPrivateConst<T>(this object obj, string name)
        {
            // Set the flags so that private and public fields from instances will be found
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
            var field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
        }
    }



}
