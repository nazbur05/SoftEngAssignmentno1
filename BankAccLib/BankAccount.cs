namespace BankAccLib;

public class BankAccount
{
    private decimal _balance;
    private bool _isClosed = false;
    private bool _isActive = true;
    private bool _isIdVerified = false;

    public decimal Balance => _balance;
    public bool IsClosed => _isClosed;
    public bool IsActive => _isActive;
    public bool IsIdVerified => _isIdVerified;

    public void Deposit(decimal amount)
    {
        if (_isClosed) throw new AccountClosedException();
        if (!_isActive) Reactivate();

        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (_isClosed) throw new AccountClosedException();
        if (!_isActive) throw new AccountInactiveException();
        if (!_isIdVerified) throw new IdNotVerifiedException();
        if (_balance < amount) throw new InvalidOperationException("Not enough funds in your account.");

        _balance -= amount;
    }

    public void VerifyId()
    {
        _isIdVerified = true;
    }

    public void CloseAccount()
    {
        _isClosed = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    private void Reactivate()
    {
        _isActive = true;
        OnReactivation();
    }

    private void OnReactivation()
    {
        Console.WriteLine("Account has been reactivated! ");
    }
}

public class AccountClosedException : Exception { }
public class AccountInactiveException : Exception { }
public class IdNotVerifiedException : Exception { }