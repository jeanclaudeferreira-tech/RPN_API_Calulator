# RPN Api project

Application client/serveur REST de calculatrice en notation polonaise inverse

### Todo

- [ ] Add log Serilog 
- [ ] Make asynchronous method
- [ ] Reuse Id after deleted stack
- [ ] Save data in a database
- [ ] Use JSON to get content of a stack or list of available stacks
- [ ] When a stack is created return the code 201 Created insted of 200

### Done ✓

- [x] Create first version
- [x] Create REST Get method : List all of operators
- [x] Create REST Get method : List available stacks
- [x] Create REST Get method : Get content of a stacks
- [x] Create REST Post method : Create new stacks
- [x] Create REST Post method : Push a new value to a stacks
- [x] Create REST Delete method : Delete a stack
- [x] Create REST Post method : Apply an operator to a stack