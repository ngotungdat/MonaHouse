class NotificationUser {
    constructor(Id, UsersId, Title, Content, NotificationId, TenantId, Created, CreatedBy, Updated, UpdatedBy, Deleted, Active, isSeen) {
        this.Id = Id;
        this.UsersId = UsersId;
        this.Title = Title;
        this.Content = Content;
        this.NotificationId = NotificationId;
        this.Created = Created;
        this.CreatedBy = CreatedBy;
        this.Updated = Updated;
        this.UpdatedBy = UpdatedBy;
        this.Deleted = Deleted;
        this.Active = Active;
        this.isSeen = isSeen;
    }
}
class UtilitiesConfig
{
    constructor(Id,Name,Price,Deleted)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price= Price;
        this.Deleted= Deleted;
    }
}