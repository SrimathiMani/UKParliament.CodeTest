//export interface PersonViewModel {
//  id: number;
//}

export class Person {
  id?: number;
  title?: string;
  firstName?: string;
  lastName?: string;
  dateOfBirth?: Date;
  gender?: any;
}

export enum Gender {
  Male = 1,
  Female = 2
}
