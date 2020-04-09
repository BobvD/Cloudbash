export class User {
    token: string;
}

export class Credentials {
    username: string;
    password: string;
}

export enum Role {
    User = 'User',
    Admin = 'Admin'
}