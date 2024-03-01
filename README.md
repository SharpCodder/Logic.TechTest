# Logic Technical Assement

This test is designed to test to assess real-world skills of C#, MVC, Razor and ajax/jQuery. You are advised to read the whole task before starting. For the sake of the assessment, you should favour "out of the box" MVC/Razor language features over "hand-rolled" solutions where possible. 

You may complete this however you see fit. Feel free to use online resources and include nuget packages as required. Also, please change/move/refactor existing code as you see fit. That being said, please note you will be expected to talk through your solution if you are selected for interview.

Consideration should be given to naming, structure/architecture and adherence to SOLID principles

The task is to create a page in the app to submit and view leave requests based on Models\LeaveViewModel. For the sake of the assessment, store the leave requests in the session

1. Clone the repository to your local machine
1. Create a partial view to show a table of leaave requests
1. Amend the Views\Leave\Index.cshml to include a form to submit the data, with a placeholder underneath for the list of submitted leave requests
1. Create controller actions to populate the form on the first visit, accept the POSTed form data and return the list of submitted leave requests

The form should validate that the email is in the correct format, all fields are required and where IsHalfDay is true the StartDate and EndDate must be the same (ie: half day may only be applied to a single day). 

Ideally, the form should submit without a page refresh, updating (only) list of leave requests. Finally, please write a unit test for the custom validation logic using a testing framework of your choice.

Please zip your solution and send to j.blake@logicinvestments.co.uk.

Thank you for your time. 