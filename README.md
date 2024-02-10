Steps:
Clone or Download the Project:

Clone the project from your repository or download the ZIP file.
Open the Project in Visual Studio:

Open Visual Studio.
Click on "Open a project or solution."
Navigate to the folder where you cloned or downloaded the project and select the .sln file.
Restore Dependencies:

Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
Run the following command to restore the project dependencies:
bash
Copy code
dotnet restore
Update Database:

Open the Package Manager Console.
Run the following commands to apply the migrations and update the database:
bash
Copy code
dotnet ef migrations add InitialMigration
dotnet ef database update
Run the Application:

Press F5 or click on the "Start Debugging" button to run the application.
The API will be hosted on a local server (typically https://localhost:5001 or http://localhost:5000).
Test the API Endpoints:

You can use tools like Postman or Swagger to test the API.
Explore the API endpoints you have implemented (e.g., create posts, get posts, like posts).
Stop the Application:

Press Shift + F5 or click on the "Stop Debugging" button to stop the application.



API Journey Documentation
Overview
The API Journey allows users to create accounts, create posts, and interact with other users through reactions and comments.

Base URL
http://jaydevportfolio.somee.com/

Endpoints
1. Create a User
Description: Creates a new user.
http://jaydevportfolio.somee.com/api/users
Method: POST
Request Body:
json
Copy code
{
  "id": 0,
  "userId": 0,
  "username": "string",
  "password": "string"
}
Note: Do not edit id and userId, leave them as 0.
Response: No specific response, but the user will be created.


2. Retrieve Users
Description: Retrieves a list of all users.
http://jaydevportfolio.somee.com/api/users
Method: GET
Response:
A list of users.


3. Create a Post
Description: Creates a new post for a specific user.
http://jaydevportfolio.somee.com/api/Posts/{userId}
Method: POST
Request Body:
json
Copy code
{
  "postId": 0,
  "title": "I love coding too come to think of it",
  "message": "testing code in the third circuit",
  "userId": 0,
  "id": 0,
  "uniqueId": "b7e896e7-60d5-4ce3-a06f-18644a140e42"
}
Note: Do not edit postId, userId, id, and uniqueId, leave them as 0 or as provided.
Response: No specific response, but the post will be created.

4. Edit Posts
Description: Edits a post for a specific Post.
http://jaydevportfolio.somee.com/api/Posts/{postId}
Method: PUT 
Request Body:
json
Copy code
{
  "postId": 0,
  "title": "I love food instead ",
  "message": "string",
  "userId": 0,
  "id": 0,
  "uniqueId": "b7e896e7-60d5-4ce3-a06f-18644a140e42"
}
Note: Do not edit postId, userId, id, and uniqueId, leave them as 0 or as provided.
only edit either title or message then else leave the other as "string"

Response: No specific response, but the post will be edited.


5. Delete Post
Description: Deletes a post for a specific post using its id.
URL: http://jaydevportfolio.somee.com/api/Posts/{postId}
Method: DELETE


Response: No specific response, but the post will be deleted.

6.  Like post (upvote a post)
Description: likes a post for a specific post using its id also as a specific user.
URL: http://jaydevportfolio.somee.com/api/Likes/post/{userId}/{postId}/addlike
Method: POST

Response: No specific response, but the post will be liked (upvoted).


7.  Dislike a post (downvote a post)
Description: Dislikes a post for a specific post using its id also as a specific user.
URL: http://jaydevportfolio.somee.com/api/Likes/post/{userId}/{postId}/addDislike
Method: POST

Response: No specific response, but the post will be Disliked (downvoted).


8. Add Comment on a Post
Description: Comments on a post for a specific post using its id also as a specific user.
URL: http://jaydevportfolio.somee.com/api/Comments/post/{userId}/{postId}
Method: POST
Request Body:
json
Copy code
{
  "id": 0,
  "commentId": 0,
  "commentText": "I love that its amazing",
  "postId": 0,
  "userId": 0
}

Note: Do not edit commentId, postId, userId, id, and uniqueId, leave them as 0 or as provided.

9. Add a like on a comment
Description: likes a post for a specific post using its id also as a specific user.
URL: http://jaydevportfolio.somee.com/api/CommentLikes/comment/{commentId}/CommentLikes
Method: POST

Response: No specific response, but the comment on a post will be liked (upvoted).

10. Dislike a comment 

Description: likes a post for a specific post using its id also as a specific user.
URL: http://jaydevportfolio.somee.com/api/CommentLikes/comment/{commentId}/CommentDislikes
Method: POST

Response: No specific response, but the comment on a post will be Disliked (upvoted).


11. Get posts that a user has created
Description: Gets all posts that a were created by specific user.
URL: http://jaydevportfolio.somee.com/api/users/user/posts/{userId}
Method: GET

Response: Returns a list of possts that were created by a specific user


12. Get posts that a user has liked
Description: Gets all posts that a were liked by specific user.
URL: http://jaydevportfolio.somee.com/api/users/user/posts/userliked/{userId}
Method: GET

Response: Returns a list of posts that were liked by a specific user


13. Get posts by a username
Description: Gets all posts by a specific user using their username.
URL: http://jaydevportfolio.somee.com/api/users/user/posts/findby/{username}
Method: GET

Response: Returns a list of posts that were created by a specific user using their username.

14. Get post details
Description: Gets all post details.
URL: http://jaydevportfolio.somee.com/api/Posts/{postId}
Method: GET



for ease of user the id's are incremented on creation( only for Specno demonstration purposes) but for security and redundancy the uniqueId is generated 


please I want this postion i can do this 
