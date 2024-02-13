import { Person } from "./person"
import { Procedure } from "./procedure"

// This most likely be spaced out into different interfaces/classes according to roles
interface Users{
  Person: Person,
  // FirstName: string,
  // MiddleName?: string,
  // LastName: string,
  // Email: string,
  // PhoneNumber: number,
  // Address: string,
  Age: number,
  Active: boolean, //Left it boolean cann change to string if it's easier
  Files?: Blob,
  Guardian?: string
  Procedure?: Procedure, //Most likely a seperate interface with a nullable option
  MedicalHistory: string,
  TeethHistory: string
}
