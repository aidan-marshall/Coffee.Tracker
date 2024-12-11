import {DisplayAllCoffeeRecords} from "../Components/ListAllCoffeeRecords";
import {AddCoffeeRecord} from "../Components/AddCoffeeRecordButton";

export function HomePage() {
  return (
    <div>
      <h1>Home Page</h1>
        <DisplayAllCoffeeRecords /> 
        <AddCoffeeRecord />
    </div>
  );
}