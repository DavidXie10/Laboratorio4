using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestLab10.MoqExamples {
    public interface NamesService {
        string GetName();
    }

    [TestClass]
    public class Example1 {
        [TestMethod]
        public void TestGetNamesFromNamesService() {
            var mock = new Mock<NamesService>();
            mock.Setup(mocked => mocked.GetName()).Returns("SuNombre");
            Assert.AreEqual("SuNombre", mock.Object.GetName());
        }
    }
}
