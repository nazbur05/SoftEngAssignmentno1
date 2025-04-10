using Xunit;
using BankAccLib;

public class BankAccTest
{
    [Fact]
    public void Deposit_IncrBalance()
    {
        var account = new BankAccount();
        account.Deposit(800);
        Assert.Equal(800, account.Balance);
    }

    [Fact]
    public void Withdraw_WithoutVerification_Throws()
    {
        var account = new BankAccount();
        account.Deposit(700);
        Assert.Throws<IdNotVerifiedException>(() => account.Withdraw(300));
    }

    [Fact]
    public void Withdraw_WithVerification_Succeeds()
    {
        var account = new BankAccount();
        account.Deposit(300);
        account.VerifyId();
        account.Withdraw(100);
        Assert.Equal(200, account.Balance);
    }

    [Fact]
    public void CloseAccount_BlocksOperations()
    {
        var account = new BankAccount();
        account.CloseAccount();
        Assert.Throws<AccountClosedException>(() => account.Deposit(150));
    }

    [Fact]
    public void DeactivatedAccount_ReactivatesOnDeposit()
    {
        var account = new BankAccount();
        account.Deactivate();
        account.Deposit(600);
        Assert.Equal(600, account.Balance);
        Assert.True(account.IsActive);
    }

    [Fact]
    public void Withdraw_OnDeactivatedAccount_Throws()
    {
        var account = new BankAccount();
        account.Deposit(120);
        account.VerifyId();
        account.Deactivate();
        Assert.Throws<AccountInactiveException>(() => account.Withdraw(50));
    }

    [Fact]
    public void CannotWithdrawMoreThanBalance()
    {
        var account = new BankAccount();
        account.Deposit(100);
        account.VerifyId();
        Assert.Throws<InvalidOperationException>(() => account.Withdraw(200));
    }
}
