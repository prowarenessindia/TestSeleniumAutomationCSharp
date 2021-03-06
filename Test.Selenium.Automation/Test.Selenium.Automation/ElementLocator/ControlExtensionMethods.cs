﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Test.Selenium.Automation.Driver;
using Test.Selenium.Automation.UiControlInterfaces;
using Test.Selenium.Automation.UiControls;
using Test.Selenium.Automation.Utilities;

namespace Test.Selenium.Automation.ElementLocator
{
    public static class ControlExtensionMethods
    {
        private static readonly Actions Actions;

        static ControlExtensionMethods()
        {
            Actions = new Actions(WebDriver.WebDriverInstance);
        }

        public static void DragDrop(this HtmlControl source, HtmlControl destination)
        {
            Actions.DragAndDrop(source.WebElement, destination.WebElement);
        }

        public static void KeyInEnter(this TextBox control)
        {
            control.SetText(Keys.Enter);
        }

        public static T FindElement<T>(this HtmlControl control, string locatorType, string locatorValue) where T : IHtmlControl, new()
        {
            var locator = Common.GetLocator(locatorType, locatorValue);

            try
            {
                var webElement = control.WebElement.FindElement(locator);
                var element = new T { WebElement = webElement };
                return element;
            }
            catch (Exception ex)
            {
                //This should be replace by some logging mechanism
                Console.Write(ex.Message);
                return new T(); 
            }
            
        }

        public static IReadOnlyCollection<T> FindElements<T>(this HtmlControl control, string locatorType, string locatorValue) where T : IHtmlControl, new()
        {
            var elements = new List<T>();
            var locator = Common.GetLocator(locatorType, locatorValue);

            try
            {
                var webElements = control.WebElement.FindElements(locator);

                elements.AddRange(webElements.Select(element => new T {WebElement = element}));
                return elements;
            }
            catch (Exception ex)
            {
                //This should be replace by some logging mechanism
                Console.Write(ex.Message);
                return null;
            }

        }
    }
}
