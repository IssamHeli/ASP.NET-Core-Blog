





1 =>  Configure the Database Connection String
Update the connection string in the appsettings.json file to match your SQL Server instance:

"ConnectionStrings": {
  "StringChainCnx": "Server=tcp:{your server database},1433;Initial Catalog={your database};Persist Security Info=False;User ID={Your User Id };Password={your password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

2 => new terminal > dotnet ef database update .

3 => dotnet build > dotnet run


##################################################################################################################





Admin views :

![Manage Posts](screenshots/GererPosts.png)

![Post Details](screenshots/DetailsPost.png)

![Create Post](screenshots/createpost.png)

![Messages](screenshots/Messages.png)

![Manage Categories](screenshots/gerercategorie.png)


Public  views : 

![Priority Clips Image 1](screenshots/priorityclipsimg1.png)

![Loading Posts](screenshots/loadingposts.png)

![Posts with Categories](screenshots/postswithcategories.png)

![About Us](screenshots/aboutus.png)

![Contact Us](screenshots/contactus.png)

![Thank You](screenshots/thankyou.png)


