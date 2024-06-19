public interface ITemporaryStorageService
{
    void StoreRfid(string rfid);
    string GetRfid();
    void ClearRfid();
    void StorePlaca(string placa);
    string GetPlaca();
    void ClearPlaca();
}

public class TemporaryStorageService : ITemporaryStorageService
{
    private string _rfid;
    private string _placa;

    public void StoreRfid(string rfid)
    {
        _rfid = rfid;
    }

    public string GetRfid()
    {
        return _rfid;
    }

    public void ClearRfid()
    {
        _rfid = null;
    }

    public void StorePlaca(string placa)
    {
        _placa = placa;
    }

    public string GetPlaca()
    {
        return _placa;
    }

    public void ClearPlaca()
    {
        _placa = null;
    }
}
