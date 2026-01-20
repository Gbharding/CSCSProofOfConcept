# CSCSProofOfConcept
The proof of concept web app for the CSCS application

Install instructions-
Ensure you have Visual Studio 2026 installed with the "ASP.NET and web development" workload addon, as well as .NET 10 installed
Running the slnx with these installed should allow you to run the project locally

Architecture-
I have 3 data classes, Projects, Items, and Distribution centers, along with SQL tables to store existing entities belonging to these classes.
Distribution centers have names, addresses, and the number of trucks they own
Items can have a distribution center associated with them, as well as specifications and names. They also have an "IsActive" boolean that tracks if the item has been discontinued. If it has, we hide the item from users but it sticks around in the database for audit purposes.
Projects are the most complex, they are what either creates new items (the proper way), moves an item, or discontinues one. They have names, types, lifecycle stage, freight strategy, price, and due dates. They also can be associated with an item if it is a project to transition an existing item, and alongside that they can have new specifications entered in order to alter the specifications of an existing item, or a distribution center entered in order to change the distribution center of an existing item. Projects also have a string for their event history. When a project finishes, we hide it from users but it sticks around in the database for audit purposes.
Objects of these classes are stored in the entity framework database which then is what is used to populate the various workflow pages for each type of item.

Tech stack-
I used ASP.NET along with Razor Pages and Entity Framework.
I chose this stack because it is similar to what I had worked with at Epic while also being lightweight and easy to set up, but flexible enough for relations between entities.

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
I included scalability & enterprise connections down below.

Scalability & enterprise connections
For a multi-tenant architecture, first and foremost individual users would need to be programmed in and authenticated. Then each user and object class would need an "owning entity" data item. A user creating a new project, item, or DC would pass their owning entity data down to the object. When populating each page, the code would need to only pull in objects with a matching owning entity. For a complex multi-tenant architecture where some users can see data from multiple entities, you would need users to have a configured list of allowed entities, and then you can pull in objects from any of the ones in that list. When populating entity down onto an object a user is creating, you would need a way to decide which one to put onto the object, be that either a configurable algorithm or you could just have the user log into the specific entity they want to represent at the start and then just pass that down. (I did a very large project with my current employer that was migrating the way we handle this from logins to a configurable algorithm)
For deep audit logging, a separate events table would be needed. I called this out in my "what I would add with more time," but my current action logging is very rudimentary and is just a big long string in the project class. This would need to be extracted into a table with atomic data (so a line key would be necessary). Then individual parts of the action can be broken up into separate data as well, like the time, user, and action taken (like accept or reject). It would also be wise to add audit history for creation, deletion, and editing of the other classes as well. Finally, items and projects can become inactive or finished by workflow means, but having this be the norm for class deletion of all classes through the delete button would help for auditing as well.