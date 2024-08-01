namespace AuthSample.Interfaces;

public interface IMdlBaseService
{
    [ApiPermission("Base", "Mdl", "Add")]
    void Add();

    [ApiPermission("Base", "Mdl", "Update")]
    void Update();

    [ApiPermission("Base", "Mdl", "List")]
    void List();

    [ApiPermission("Base", "Mdl", "Read")]
    void Get(int id);

    [ApiPermission("Base", "Mdl", "Delete")]
    void Delete();
}
