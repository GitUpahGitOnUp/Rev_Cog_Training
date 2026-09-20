namespace bankLIB.Entities;

// limits a transaction to only these exact vals/spellings so no one fat-fingers a typo
public enum TransactionType
{
    Deposit,
    Withdraw,
    TransferIn,
    TransferOut
}