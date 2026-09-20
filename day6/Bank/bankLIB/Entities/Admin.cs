namespace bankLIB.Entities;

// A : U - A inherits from U
// inherits login fields + behavior but gets special Admin capabilities
// breaking out admin from customer with a user abstraction will help make sure that a customer can't do admin stuff

public class Admin : User
{
    
}