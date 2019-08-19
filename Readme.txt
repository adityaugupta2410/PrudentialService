Hello Readers,
This solution contains two projects :
  1. WEBAPI Service Project
  2. Unit Test Project

Build Instructions -
 -Kindly restore packages from Nuget Package Manager before attempting a build.

 -The Input File in the InputFiles Folder can be changed to include multiple cities (in the predefined JSON format only).

 - After Opening in VS 2017, kindly build and run with the WeatherDataAPI Project as start up project. 

 - Navigate to URL : localhost:<your_port_number>//api/WeatherData

 -The Output message will indicate the path of generated files.


Changes that need to  be made for unit Test Project to run -
 -Change physical path mentioned in Keys of config file of unit test project. This is done as HttpCurrentContext is always null when initializing the service from Unit Test Project.

Points to note -

 -Have used the naming convention for Folder as Output_<Date in dd/mm/yyyy format>
	-Each folder will contain files of individual cities of that date.

 -Have Logged each request made to the WEBAPI in {root}/APILogs folder.

 - Solution contains WEBAPI Service, if this needs to run on regular time interval a console application consuming this service would need to be created and scheduled    in Task manager.

