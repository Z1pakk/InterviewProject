export interface IUser {
  id: string;
  email: string;

  firstName: string;
  lastName: string;

  fullName: string;

  roleName: string;

  isAdmin: boolean;

  password?: string;
}
