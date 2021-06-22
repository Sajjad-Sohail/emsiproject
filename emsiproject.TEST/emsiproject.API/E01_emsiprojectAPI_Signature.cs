using emsiproject.Controllers;
using emsiproject.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace emsiproject.TEST.emsiproject.API
{
    [TestClass]
    public class E01_emsiprojectAPI_Signature
    {
        [TestMethod]
        public void T00_01_Must_Have_One_Constructor()
        {
            //Arrange
            Type getTypeInfo = new AreasController(null).GetType();

            //Act
            ConstructorInfo[] testObject = getTypeInfo.GetConstructors();

            //Assert
            Assert.AreEqual(2, testObject.Length);
        }

        [TestMethod]
        public void T00_02_Constructor_has_exactly_zero_parameters()
        {
            //Arrange
            Type getTypeInfo = new AreasController(null).GetType();

            //Act
            ConstructorInfo[] testObject = getTypeInfo.GetConstructors();
            ParameterInfo[] getAreasConstructor_paramInfo = testObject[0].GetParameters();

            //Assert
            Assert.AreEqual(1, getAreasConstructor_paramInfo.Length);
        }

        
        [TestMethod]
        public void T00_03_Constructor_ParameterType_is_DatabaseConfig()
        {
            //Arrange
            Type getTypeInfo = new AreasController(null).GetType();

            //Act
            ConstructorInfo[] testObject = getTypeInfo.GetConstructors();
            ParameterInfo[] getAreasConstructor_paramInfo = testObject[0].GetParameters();

            //Assert
            Assert.AreEqual(typeof(DatabaseConfig), getAreasConstructor_paramInfo[0].ParameterType);
        }

        [TestMethod]
        public void T00_05_Function_Get_Exists()
        {
            //Arrange
            Type getTypeInfo = new AreasController(null).GetType();
            string expectedParameterName = "databaseConfig";

            //Act
            ConstructorInfo[] testObject = getTypeInfo.GetConstructors();
            ParameterInfo[] getAreasConstructoer_1_paramInfo = testObject[1].GetParameters();

            //Assert
            Assert.AreEqual(expectedParameterName, getAreasConstructoer_1_paramInfo[0].Name);
        }

        //does the function exist
        //function return type
        //has exactly one parameter
        //parameter type
        //parameter name

       

       

    }
}
