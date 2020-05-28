# CODING-TEST-NET-CORE

# CONTEXT PROPERTY

Id 
ExpiredDate 
Title 
Description 
CompletePercentage 
Status 

# ENDPOINT API

- Get All Todo’s
[METHOD GET: api/TodoItems]

- Get Specific Todo
[METHOD GET: api/TodoItems/5]

- Get Incoming ToDo (for today/next day/current week)
[METHOD GET: api/TodoItems/Today]
[METHOD GET: api/TodoItems/Tomorow]
[METHOD GET: api/TodoItems/ThisWeek]

- Create Todo
[METHOD POST: api/TodoItems]

- Update Todo
[METHOD PUT: api/TodoItems/5]

- Set Todo percent complete
[METHOD PUT: api/TodoItems/5
(add query status: "complete")]

- Delete Todo
[METHOD DELETE: api/TodoItems/5]

- Mark Todo as Done
[METHOD POST: api/TodoItems/5/Done
