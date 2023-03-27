// See https://aka.ms/new-console-template for more information
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Net.Mime.MediaTypeNames;


Console.ReadKey();

namespace TechnicalAssessment
{


    public class ReverseParagraph
    {

        public ReverseParagraph()
        {

        }


        public List<string> ReverseSentences(List<string> Sentences)
        {
            List<string> cases = new List<string>();

            try
            {
                validateCaseNumberIndictor(Sentences);

                int numCases = int.Parse(Sentences[0].Trim()) ;

                for (int i = 1; i <= numCases; i++)
                {
                    StringBuilder Case = new StringBuilder($"Case {i}: ");
                    
                    List<string> Words = Sentences[i].Split(' ').Reverse().ToList();
                    
                    string reversedSentence = Words.Aggregate((x, y) => x + " " + y);

                    ValidateReversedSentence(reversedSentence);

                    Case.Append(reversedSentence);

                    cases.Add(Case.ToString());
                }

            }
            catch (Exception)
            {

                throw;
            }

            return cases;

        }

        private void validateCaseNumberIndictor(List<string> Sentences)
        {

            // Is the Case Number indicator on the first line valid?
            if (!int.TryParse(Sentences[0].Trim(), out int numCases))
            {
                throw new Exception(EnumErrorMessage.FirstLineIndicatorValueInValid.ToString());
            }

                //Does the number of cases on the first line match the actual nmumber of cases?
            if (Sentences.Count != numCases + 1)
            {
                throw new Exception(EnumErrorMessage.FirstLineIndicatorValueMismatch.ToString());
            }           
        }
        private void ValidateReversedSentence(string reversedSentence) 
        {
            try
            {
                //The number of letters in a line cannot be less than 1
                if (reversedSentence.Replace(" ", "").Length < 1)
                {
                    throw new Exception(EnumErrorMessage.SentenceNotMinLength.ToString());
                }

                //The number of letters in a line cannot exceed 25
                if (reversedSentence.Replace(" ", "").Length > 25)
                {
                    throw new Exception(EnumErrorMessage.SentenceMaxLengthExceeded.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }


    public enum EnumErrorMessage
    {
        SentenceMaxLengthExceeded,
        SentenceNotMinLength,
        FirstLineIndicatorValueInValid,
        FirstLineIndicatorValueMismatch,
        CaseReversalOutputInvalid,
    }

    [TestClass]
    public class ReverseParagraphUnitTest
    {

        [TestMethod]
        public void SentencesReversedTest()
        {
            List<string> cases = new List<string>() { "3", "this is a test", "foobar", "all your base" };

            try
            {
                ReverseParagraph reverseParagraph = new();


                cases = reverseParagraph.ReverseSentences(cases);

            }
            catch (Exception ex)
            {
                //This was not supposed to happen
                Assert.AreEqual(ex.Message, "The number of letters in a line cannot exceed 25");

            }
                             
            if (cases[0] == "Case 1: test a is this" &&
                cases[1] == "Case 2: foobar" &&
                cases[2] == "Case 3: base your all")
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                //Fail the test
                Assert.AreNotEqual(1, 1);
            }


        }

        [TestMethod]
        public void SentenceExceedsMaxLengthTest()
        {
            try
            {
                ReverseParagraph reverseParagraph = new();

                List<string> cases = new List<string>() { "3", "hello world ", "This is nice", "Increase the numnber of letters in a sentence to be more than 25" };
                cases = reverseParagraph.ReverseSentences(cases);
                //If the test got this far it has passed, no exceptions have been thrown
                Assert.AreEqual(1, 1);
            }
            catch (Exception ex)
            {
                Enum.TryParse(ex.Message, out EnumErrorMessage enumErrorMessage);
                Assert.AreEqual(enumErrorMessage, EnumErrorMessage.SentenceMaxLengthExceeded);

            }


        }


        [TestMethod]
        public void SentenceShorterThanMinLengthTest()
        {
            try
            {
                ReverseParagraph reverseParagraph = new();

                List<string> cases = new List<string>() { "3", "", "This is nice", "Increase the number of letters in a sentence to be more than 25" };
                cases = reverseParagraph.ReverseSentences(cases);
                //If the test got this far it has failed because there is an empty string in one of the cases
                Assert.AreEqual(1, 0);
            }
            catch (Exception ex)
            {
                Enum.TryParse(ex.Message, out EnumErrorMessage enumErrorMessage);
                Assert.AreEqual(enumErrorMessage, EnumErrorMessage.SentenceNotMinLength);
            }


        }

        [TestMethod]
        public void FirstLineIndicatorInvalidTest()
        {
            try
            {
                ReverseParagraph reverseParagraph = new();

                List<string> cases = new List<string>() { "gwew", "hello world ", "This is nice", "Increase the numnber of letters in a sentence to be more than 25" };
                cases = reverseParagraph.ReverseSentences(cases);
                //If the test got this far , the first line was somehow converted to a number and the test is a failure
                Assert.AreEqual(1, 0);
            }
            catch (Exception ex)
            {
                Enum.TryParse(ex.Message, out EnumErrorMessage enumErrorMessage);
                Assert.AreEqual(enumErrorMessage, EnumErrorMessage.FirstLineIndicatorValueInValid);
            }            


        }

        [TestMethod]
        public void FirstLineIndicatorInacuracyTest()
        {
            try
            {
                ReverseParagraph reverseParagraph = new();

                List<string> cases = new List<string>() { "7", "hello world ", "This is nice", "Increase the numnber of letters in a sentence to be more than 25" };
                cases = reverseParagraph.ReverseSentences(cases);

                //No exception means the error was not captured
                Assert.AreEqual(1, 0);
            }
            catch (Exception ex)
            {
                //this exception must be thrown for the test to pass
                Enum.TryParse(ex.Message, out EnumErrorMessage enumErrorMessage);

                Assert.AreEqual(enumErrorMessage, EnumErrorMessage.FirstLineIndicatorValueMismatch);
            }

            
            
        }





    }


}