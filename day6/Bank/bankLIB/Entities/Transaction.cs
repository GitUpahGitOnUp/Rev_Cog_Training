using System.ComponentModel.DataAnnotations.Schema;

namespace bankLIB.Entities;

// this is one recorded event on an acc: dep, withdr, etc

public class Transaction
{
    #region Properties

    public int TransactionId {get; set;}
    
    // accNo of acc the trans. happened ON
    // and is stored separately as a value type and not an Account reference
    // so that the transaction history stays stable even if the acc. obj. changes in the futur
    public int AccNo {get; set;}

    public TransactionType Type {get; set;}

    public decimal Amount {get; set; }

    // the acc. balance right after this transaction occurs
    public decimal BalanceAfterTransaction {get; set;}

    // timestamp for when it happens, default to now
    // this is a transaction obj so it doesn't have to be manually set ea. event
    public DateTime Timestamp {get; set;} = DateTime.Now;

    #endregion
}