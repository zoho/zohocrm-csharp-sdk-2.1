using System;

using System.Collections.Generic;

using System.Reflection;

using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.CustomViews;
using Com.Zoho.Crm.API.Layouts;
using Com.Zoho.Crm.API.Users;

using Com.Zoho.Crm.API.Util;

using Com.Zoho.Crm.API.Wizards;

using Newtonsoft.Json;

using static Com.Zoho.Crm.API.Wizards.WizardsOperations;

using APIException = Com.Zoho.Crm.API.Wizards.APIException;

using ResponseHandler = Com.Zoho.Crm.API.Wizards.ResponseHandler;

using ResponseWrapper = Com.Zoho.Crm.API.Wizards.ResponseWrapper;

namespace Com.Zoho.Crm.Sample.Wizards
{
    public class Wizard
    {
        public static void GetWizards()
        {
            //Get instance of WizardsOperations Class
            WizardsOperations wizardsOperations = new WizardsOperations();

            //Call GetWizards method
            APIResponse<ResponseHandler> response = wizardsOperations.GetWizards();

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the list of obtained Wizard instances
                        List<API.Wizards.Wizard> wizards = responseWrapper.Wizards;

                        foreach (API.Wizards.Wizard wizard in wizards)
                        {
                            //Get the CreatedTime of each Wizard
                            Console.WriteLine("Wizard CreatedTime: " + wizard.CreatedTime);

                            //Get the PermissionType of each Wizard
                            Console.WriteLine("Wizard ModifiedTime: " + wizard.ModifiedTime);

                            //Get the manager User instance of each Wizard
                            API.Modules.Module module = wizard.Module;

                            //Check if manager is not null
                            if(module != null)
                            {
                                //Get the Name of the Manager
                                Console.WriteLine("Wizard Module APIName: " + module.APIName);

                                //Get the ID of the Manager
                                Console.WriteLine("Wizard Module Id: " + module.Id);
                            }

                            //Get the Name of each Wizard
                            Console.WriteLine("Wizard Name: " + wizard.Name);

                            //Get the modifiedBy User instance of each Wizard
                            API.Users.User modifiedBy = wizard.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the Name of the modifiedBy User
                                Console.WriteLine("Wizard Modified By User-Name: " + modifiedBy.Name);

                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Wizard Modified By User-ID: " + modifiedBy.Id);
                            }

                            //Get the array of Profile instance each Wizard
                            List<API.Profiles.Profile> profiles = wizard.Profiles;

                            //Check if profiles is not null
                            if(profiles != null)
                            {
                                foreach(API.Profiles.Profile profile in profiles)
                                {
                                    //Get the Name of each Profile
                                    Console.WriteLine("Wizard Profile Name: " + profile.Name);

                                    //Get the ID of each Profile
                                    Console.WriteLine("Wizard Profile ID: " + profile.Id);
                                }
                            }

                            //Get the Active of each Wizard
                            Console.WriteLine("Wizard Active: " + wizard.Active);

                            //Get the array of Container instance each Wizard
                            List<API.Wizards.Container> containers = wizard.Containers;

                            //Check if containers is not null
                            if(containers != null)
                            {
                                foreach(API.Wizards.Container container in containers)
                                {
                                    //Get the array of Layout instance each Wizard
                                    API.Layouts.Layout layout = container.Layout;

                                    //Check if layout is not null
                                    if(layout != null)
                                    {
                                        //Get the Name of Layout
                                        Console.WriteLine("Wizard Container Layout Name: " + layout.Name);

                                        //Get the ID of Layout
                                        Console.WriteLine("Wizard Container Layout ID: " + layout.Id);
                                    }

                                    //Get the ID of each Container
                                    Console.WriteLine("Wizard Container ID: " + container.Id);
                                }
                            }

                            //Get the ID of each Wizard
                            Console.WriteLine("Wizard ID: " + wizard.Id);

                            //Get the createdBy User instance of each Wizard
                            User createdBy = wizard.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Name of the createdBy Wizard
                                Console.WriteLine("Wizard Created By User-Name: " + createdBy.Name);

                                //Get the ID of the createdBy Wizard
                                Console.WriteLine("Wizard Created By User-ID: " + createdBy.Id);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if(responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void GetWizardById(long? id, string layoutId)
        {
            //Get instance of WizardsOperations Class
            WizardsOperations wizardsOperations = new WizardsOperations();

            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(GetWizardbyIDParam.LAYOUT_ID, layoutId);

            //Call GetWizardById method
            APIResponse<ResponseHandler> response = wizardsOperations.GetWizardById(id, paramInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the list of obtained Wizard instances
                        List<API.Wizards.Wizard> wizards = responseWrapper.Wizards;

                        foreach (API.Wizards.Wizard wizard in wizards)
                        {
                            //Get the CreatedTime of each Wizard
                            Console.WriteLine("Wizard CreatedTime: " + wizard.CreatedTime);

                            //Get the PermissionType of each Wizard
                            Console.WriteLine("Wizard ModifiedTime: " + wizard.ModifiedTime);

                            //Get the manager User instance of each Wizard
                            API.Modules.Module module = wizard.Module;

                            //Check if manager is not null
                            if(module != null)
                            {
                                //Get the Name of the Manager
                                Console.WriteLine("Wizard Module APIName: " + module.APIName);

                                //Get the ID of the Manager
                                Console.WriteLine("Wizard Module Id: " + module.Id);
                            }

                            //Get the Name of each Wizard
                            Console.WriteLine("Wizard Name: " + wizard.Name);

                            //Get the modifiedBy User instance of each Wizard
                            API.Users.User modifiedBy = wizard.ModifiedBy;

                            //Check if modifiedBy is not null
                            if(modifiedBy != null)
                            {
                                //Get the Name of the modifiedBy User
                                Console.WriteLine("Wizard Modified By User-Name: " + modifiedBy.Name);

                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Wizard Modified By User-ID: " + modifiedBy.Id);
                            }

                            //Get the array of Profile instance each Wizard
                            List<API.Profiles.Profile> profiles = wizard.Profiles;

                            //Check if profiles is not null
                            if(profiles != null)
                            {
                                foreach(API.Profiles.Profile profile in profiles)
                                {
                                    //Get the Name of each Profile
                                    Console.WriteLine("Wizard Profile Name: " + profile.Name);

                                    //Get the ID of each Profile
                                    Console.WriteLine("Wizard Profile ID: " + profile.Id);
                                }
                            }

                            //Get the Active of each Wizard
                            Console.WriteLine("Wizard Active: " + wizard.Active);

                            //Get the array of Container instance each Wizard
                            List<Container> containers = wizard.Containers;

                            //Check if containers is not null
                            if(containers != null)
                            {
                                foreach(Container container in containers)
                                {
                                    //Get the array of Layout instance each Wizard
                                    Layout layout = container.Layout;

                                    //Check if layout is not null
                                    if(layout != null)
                                    {
                                        //Get the Name of Layout
                                        Console.WriteLine("Wizard Container Layout Name: " + layout.Name);

                                        //Get the ID of Layout
                                        Console.WriteLine("Wizard Container Layout ID: " + layout.Id);
                                    }

                                    ChartData chartData = container.ChartData;

                                    if(chartData != null)
                                    {
                                        List<Node> nodes = chartData.Nodes;

                                        if(nodes != null)
                                        {
                                            foreach(Node node in nodes)
                                            {
                                                Console.WriteLine("Wizard Container ChartData Node PosY: " + node.PosY);

                                                Console.WriteLine("Wizard Container ChartData Node PosX: " + node.PosX);

                                                Console.WriteLine("Wizard Container ChartData Node StartNode: " + node.StartNode);

                                                Screen screen = node.Screen;

                                                if(screen != null)
                                                {
                                                    Console.WriteLine("Wizard Container ChartData Node Screen DisplayLabel: " + screen.DisplayLabel);

                                                    Console.WriteLine("Wizard Container ChartData Node Screen ID: " + screen.Id);
                                                }
                                            }
                                        }

                                        List<Connection> connections = chartData.Connections;

                                        if(connections != null)
                                        {
                                            foreach(Connection connection in connections)
                                            {
                                                Button sourceButton = connection.SourceButton;

                                                if(sourceButton != null)
                                                {
                                                    PrintButton(sourceButton);
                                                }

                                                Screen targetScreen = connection.TargetScreen;

                                                if(targetScreen != null)
                                                {
                                                    PrintScreen(targetScreen);
                                                }
                                            }
                                        }

                                        Console.WriteLine("Wizard Container ChartData CanvasWidth: " + chartData.CanvasWidth);

                                        Console.WriteLine("Wizard Container ChartData CanvasHeight: " + chartData.CanvasHeight);
                                    }

                                    List<Screen> screens = container.Screens;

                                    if(screens != null)
                                    {
                                        foreach(Screen screen in screens)
                                        {
                                            PrintScreen(screen);
                                        }
                                    }

                                    //Get the ID of each Container
                                    Console.WriteLine("Wizard Container ID: " + container.Id);
                                }
                            }

                            //Get the ID of each Wizard
                            Console.WriteLine("Wizard ID: " + wizard.Id);

                            //Get the createdBy User instance of each Wizard
                            User createdBy = wizard.CreatedBy;

                            //Check if createdBy is not null
                            if(createdBy != null)
                            {
                                //Get the Name of the createdBy Wizard
                                Console.WriteLine("Wizard Created By User-Name: " + createdBy.Name);

                                //Get the ID of the createdBy Wizard
                                Console.WriteLine("Wizard Created By User-ID: " + createdBy.Id);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if(responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException) responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: " );

                        //Get the details map
                        foreach(KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        private static void PrintScreen(Screen screen)
        {
            Console.WriteLine("Screen Id: " + screen.Id);

            Console.WriteLine("Screen DisplayLabel: " + screen.DisplayLabel);

            List<Segment> segments = screen.Segments;

            if(segments != null)
            {
                foreach(Segment segment in segments)
                {
                    PrintSegment(segment);
                }
            }
        }

        private static void PrintSegment(Segment segment)
        {
            Console.WriteLine("Segment Id: " + segment.Id);

            Console.WriteLine("Segment SequenceNumber: " + segment.SequenceNumber);

            Console.WriteLine("Segment DisplayLabel: " + segment.DisplayLabel);

            Console.WriteLine("Segment Type: " + segment.Type);

            Console.WriteLine("Segment ColumnCount: " + segment.ColumnCount);

            List<API.Fields.Field> fields = segment.Fields;

            if(fields != null)
            {
                foreach(API.Fields.Field field in fields)
                {
                    Console.WriteLine("Segment Field SequenceNumber: " + field.SequenceNumber);

                    Console.WriteLine("Segment Field APIName: " + field.APIName);

                    Console.WriteLine("Segment Field Id: " + field.Id);
                }
            }

            List<Button> buttons = segment.Buttons;

            if(buttons != null)
            {
                foreach(Button button in buttons)
                {
                    if(button != null)
                    {
                        PrintButton(button);
                    }
                }
            }
        }

        private static void PrintButton(Button button)
        {
            Console.WriteLine("Button Id: " + button.Id);

            Console.WriteLine("Button SequenceNumber: " + button.SequenceNumber);

            Console.WriteLine("Button DisplayLabel: " + button.DisplayLabel);

            Criteria criteria = button.Criteria;

            //Check if criteria is not null
            if(criteria != null)
            {
                PrintCriteria(criteria);
            }

            Screen targetScreen = button.TargetScreen;

            if(targetScreen != null)
            {
                Console.WriteLine("Button TargetScreen DisplayLabel: " + targetScreen.DisplayLabel);

                Console.WriteLine("Button TargetScreen Id: " + targetScreen.Id);
            }

            Console.WriteLine("Button Type: " + button.Type);

            Console.WriteLine("Button Color: " + button.Color);

            Console.WriteLine("Button Shape: " + button.Shape);

            Console.WriteLine("Button BackgroundColor: " + button.BackgroundColor);

            Console.WriteLine("Button Visibility: " + button.Visibility);

            Transition transition = button.Transition;

            if(transition != null)
            {
                Console.WriteLine("Button Transition Name: " + transition.Name);

                Console.WriteLine("Button Transition Id: " + transition.Id);
            }
        }

        private static void PrintCriteria(Criteria criteria)
        {
            if( criteria.Comparator != null)
            {
                //Get the Comparator of the Criteria
                Console.WriteLine("Criteria Comparator: " + criteria.Comparator.Value);
            }

            //Get the Field of the Criteria
            API.Fields.Field field = criteria.Field;

            if(field != null)
            {
                Console.WriteLine("Criteria Field: " + field.APIName);

                Console.WriteLine("Criteria Field: " + field.Id);
            }

            //Get the Value of the Criteria
            Console.WriteLine("Criteria Value: " + criteria.Value);

            // Get the List of Criteria instance of each Criteria
            List<Criteria> criteriaGroup = criteria.Group;

            if(criteriaGroup != null)
            {
                foreach(Criteria criteria1 in criteriaGroup)
                {
                    PrintCriteria(criteria1);
                }
            }

            if(criteria.GroupOperator != null)
            {
                //Get the Group Operator of the Criteria
                Console.WriteLine("Criteria Group Operator: " + criteria.GroupOperator.Value);
            }
        }
    }
}
