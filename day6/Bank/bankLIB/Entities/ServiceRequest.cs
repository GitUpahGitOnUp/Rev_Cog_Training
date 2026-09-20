namespace bankLIB.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// generic cust. req. that requires admin aproval

public class ServiceRequest
{
    #region Properties
    // explicit primary key as RequestId doesn't match EF's auto-dectect. naming pattern
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int RequestId {get; set;}

    public int AccNo {get; set;} // this is a value type, not an acc obj so that state at a particular time is captured, unlike a ref obj. which will
                                        // change. This value type is pointing at the accNo row in Accounts table (fk)

    public ServiceRequestType Type {get; set;}

    public ServiceRequestStatus Status {get; set;} = ServiceRequestStatus.Pending; // set automatically to pending so I don't have to remmeber later

    public DateTime DateRequested {get; set;} = DateTime.Now;


    #endregion
}