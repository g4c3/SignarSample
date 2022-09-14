export interface IUserNotification {
    senderId: string;
    userId?: string;
    userIds?: string[];
    message: string;
}