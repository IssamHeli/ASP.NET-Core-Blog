





1. Configure the Database Connection String <br>
Update the connection string in the appsettings.json file to match your SQL Server instance:<br>

json : <br>
"ConnectionStrings": { <br>
  "StringChainCnx": "Server=tcp:{your server database},1433;Initial Catalog={your database};Persist Security Info=False;User ID={Your User Id};Password={your password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}<br>

-------"Note: Make sure you have all necessary NuGet packages installed and the correct SDK version. You can verify this in the Technexa.csproj file." ----- <br>

2. Apply Database Migrations :<br>
  => Open a terminal in your project folder and run the following command to apply migrations and create the database schema: <br>
    dotnet ef database update<br>

3. Add Initial Admin Information<br>
  => Log in to your Microsoft SQL Server and manually add an initial admin record to the database. Use a strong username and password for security.<br>

4. Build and Run the Project<br>
  => Run the following commands in your terminal to build and launch the project: <br>
    dotnet build<br>
    dotnet run<br>
  => Access the application in your browser at your http://localhost:5000 or https://localhost:5001 or other <br>

5. Admin Login<br>
  => Go to the admin login page at:<br>
    http://localhost/admin/login<br>
    Log in using the initial admin credentials you added in step 3.<br>



6. Create Categories<br>
  => Navigate to the category creation page: <br>
    http://localhost/categorie/create<br>
  => Add categories as needed. Make sure not to use duplicate IDs, as the database does not have auto-increment set for this table.<br>
  --"Note: Ensure each category has a unique ID." --<br>

7. Create Posts<br>
    => Navigate to the post creation page: <br>
    http://localhost/post/create<br>
    => Fill in the required fields. For videos, ensure you have a YouTube channel. Provide the following:<br>
    * SrcYoutubeVideo: The embedded URL of your YouTube video.<br>
    Example: https://www.youtube.com/embed/x-e6G-c6xzI?si=w1ft9Moet3IDF6ly<br>

    * SrcImage: The thumbnail URL for your video.<br>
    Example: x-e6G-c6xzI/mq2.jpg?sqp=CICi3bIG-oaymwEmCMACELQB8quKqQMa8AEB-AGMAoAC4AOKAgwIABABGHIgUyhAMA8=&rs=AOn4CLDvL0EPQjV6SuwlcVC5ZTYhOtkdmg<br>


---"Notes:
Ensure your YouTube video URL and thumbnail URL are valid.<br>
Test the application thoroughly to confirm all functionalities are working as expected.<br>
If you encounter issues, feel free to ask for assistance! "---

#############################################################





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


