# Tools Used 
* C#
* F#
* SQL
* MySQL
* Visual Studio

# Description
This project is a Windows Forms Application. The application allows the user to request different types of information from a database.
There are twenty requests which are divided by the methods used to provide the information. There is a total of two methods. The first method is 
the use of an F# library. This library reads data from a specific CSV file, and each request calls an F# function that provides the request
with its intended information. The second method is the use of a SQL connection. The application will attempt to make a connection to a SQL database.
If the connection is successfull, each request will trigger a C# function that calls a SQL query, and returns the information returned by the query. 


# F# Method
![fsharppic](https://user-images.githubusercontent.com/33674827/117917386-92b01900-b2ae-11eb-8073-6c7ca4247014.PNG)

# SQL Method
![csharppic](https://user-images.githubusercontent.com/33674827/117917500-d86ce180-b2ae-11eb-9c58-cb4eb6d18381.PNG)

