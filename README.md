





1 =>  Configure the Database Connection String
Update the connection string in the appsettings.json file to match your SQL Server instance:

"ConnectionStrings": {
  "StringChainCnx": "Server=tcp:{your server database},1433;Initial Catalog={your database};Persist Security Info=False;User ID={Your User Id };Password={your password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

2 => new terminal > dotnet ef database update .

3 => dotnet build > dotnet run

screenshots : 


Admin views : 
![Manage Posts](screenshots/Gerer Posts.png)
![Post Details](screenshots/Details Post.png)
![Create Post](screenshots/create post.png)
![Messages](screenshots/Messages.png)
![Manage Categories](screenshots/gerer categorie.png)


Public  views : 

![Priority Clips Image 1](screenshots/priorityclipsimg1.png)
![Loading Posts](screenshots/loading posts.png)
![Posts with Categories](screenshots/posts with categories.png)
![About Us](screenshots/aboutus.png)
![Contact Us](screenshots/contactus.png)
![Thank You](screenshots/thank you .png)


