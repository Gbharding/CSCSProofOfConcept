# CSCSProofOfConcept
The proof of concept web app for the CSCS application

Install instructions-
Ensure you have Visual Studio 2026 installed with the "ASP.NET and web development" workload addon, as well as .NET 10 installed
Running the slnx with these installed should allow you to run the project locally

Architecture/tech stack-
I used ASP.NET along with Razor Pages and Entity Framework.
I chose this stack because it is similar to what I had worked with at Epic while also being lightweight, easy to set up, and flexible enough for the simple database I needed.

What I would improve with more time-
I ended up using a lot of static classes for holding temporary data between views. Static classes are annoying to test and maintain so I would opt for a more integrated cache.
I didn't end up making a database table for individual users, which is part of why there isn't a view for "my projects" vs "all projects" etc. I would want to support this if I had more time
My project history logs is very rudimentary, just a big string that I add onto as project steps advance. I would want to make a proper database table with a dual primary key in order to hold actual history metadata in a more sustainable and readable format.
I really wanted to add a dark mode option but didn't end up having enough time.
While I turned off buttons and links based on the logged-in user, you can still get to restricted pages by manually typing in the url for those pages. I would fix this with proper user authentication.
I didn't add an admin user, as for the sake of my datamodel the SSM had enough privelages that they were effectively my admin user while testing.

Part 2 notes-
I added stateful asynchronous handoffs between roles (in fact the majority of my time working on the project was refining this part)
I added project activity logging
I attempted to include as much of the "Sequential workflow" steps as I could given the scope of my datamodel
Each role has access to the different pages, configured loosely based on the privelage control matrix. The pages change dynamically based on the logged in user and certain buttons or links will be accesible or not depending on your user.
I did a bunch of UI enhancement, I got very carried away with CSS tuning, sorry