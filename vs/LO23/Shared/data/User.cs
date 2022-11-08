using System;
using LigthUser;

//à faire hériter de ligth user

public class User : LightUser
{
    public login: string
    public password: string
    public status: enum
    public firstname : string
    public age: int


 
    public User(int idt = 1, string userNamet = "usernametest", string imaget = "imagetest",string logint, string passwordt, string statust, string firstnamet, int aget)
    {
        id = idt;
        userName = userNamet;
        image = imaget;
        
        login = logint;
        password = passwordt;
        status = statust;
        firstname = firstnamet;
        age = aget;
    }

}
