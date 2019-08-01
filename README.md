# BusScheduler
Project Description: BusScheduler is a ReactJS web application. All the API's and services are built in .NET Core 3.0 framework.
The application uses CORS (Cross Origin Resouce Sharing) for all the communication between the Web Application and the API. The reason 
for using CORS is the WebApp and the API run locally on different ports, with out CORS it would not be possible to make REST API calls 
across different ports.The application also uses Websockets to display updated bus arrival times for a selected bus stop. 

Pre-Requisites:
  .NET Core 3.0
  Visual Studio 
  NodeJS
  
Project Target Framwork: The BusScheduleWebApp solution has 4 projects. All the projects are developed targetting the 
.NET Core 3.0 framework. 

Local environement setup: 
  In order to sucessfully to build and run the project locally, please follow the instructions below.
  1. Download and install the latest .NET Core 3.0 sdk.
  2. Download and install Node JS.
  3. Since Core 3.0 is still in preview mode, you have to enable preview mode of .NET Core in VS. For VS 2019 this option can be 
    found under tools --> options --> Preview Features. For earlier versions it can be found under tools --> options --> Projects and         Solutions. Here check the checkbox "Use previews of .NET Core SDK". 
  4. Restart VS.
    
Local Build Instructions: 
  1. After sucessfully completing the above steps. Clone the repository to your local machine.
  2. Open the solution in VS. You should see 4 projects. BusScheduleAPI, BusScheduleServicesTests, BusScheduleService, BusScheduleWebApp
  3. Just as a sanity check clean solution and build the solution. If all the above steps have been followed then you should have a 
    sucessful build.
  4. Please check the launcSettings.json under properties for both BusScheduleAPI and BusScheduleWebApp. For BusScheduleAPI the       application
    URL should be "applicationUrl": "http://localhost:62673", and for BusScheduleWebApp "applicationUrl": "http://localhost:60347". If the port
    numbers dont match then please copy them from here.
  5. Next we have to configure our solution to have multiple startup projects. Right click on solution and select "set startup projects". 
      Here select BusScheduleAPI and BusScheduleWebApp and then apply your changes.
  6. Now you should be able to start the project locally from VS.
    

