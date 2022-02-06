using System;
using Xunit;

namespace GradeBook.Tests
{
    
    public delegate string WriteLogDelegate(string logMessage);
    
    public class TypeTests
    {
        int count = 0;
        
        [Fact]
        public void WriteLogDelegateCanPointTo()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }
        
        private string IncrementCount(string message)
        {
            count ++;
            return message;
        }
        
        private string ReturnMessage(string message)
        {
            count ++;
            return message;
        }
        
        [Fact]
        public void StingsBehaveLikeValueTypes()
        {
            string name = "Scott";
            var upper = MakeUppercase(name);

            Assert.Equal("Scott", name);
            Assert.Equal("SCOTT", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }
        
        // [Fact]
        // public void Test1()
        // {
        //     var x = GetInt();
        //     SetInt(ref x); // ref creates a pointer to x, and allows it to be changed

        //     Assert.Equal(42, x);
        // }

        // private int SetInt(ref Int32 z) // adding ref lets c# know that we are looking at the reference of x to change it's value
        // {
        //     z = 42;
        // }

        // pirvate int GetInt() 
        // {
        //     return 3;
        // }
        
        [Fact]
        public void CSharpCanPassByRef()
        {
            //Arrange Section         
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");          

            //Assert Section
            Assert.Equal("New Name", book1.Name);
        }
        //ref added here allows you to access memory location of the variable
        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            //This line is copying the value of the book and creating a new object
            book = new InMemoryBook(name);
        }
        
        [Fact]
        public void CSharpIsPassByValue()
        {
            //Arrange Section         
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");          

            //Assert Section
            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            //This line is copying the value of the book and creating a new object
            book = new InMemoryBook(name);
            book.Name = name;
        }
        
        [Fact]
        public void CanSetNameFromReference()
        {
            //Arrange Section         
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");          

            //Assert Section
            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
                book.Name = name;
        }
        
        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            //Arrange Section         
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");          

            //Assert Section
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVariablesCanReferenceSameObject()
        {
            //Arrange Section         
            var book1 = GetBook("Book 1");
            var book2 = book1;          

            //Assert Section
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
