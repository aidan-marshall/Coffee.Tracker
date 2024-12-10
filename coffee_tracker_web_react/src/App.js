import logo from './logo.svg';
import './App.css';
import { DisplayAllCoffeeRecords } from './Components/CoffeeRecords.js';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Coffee Tracker</h1>
        <DisplayAllCoffeeRecords />
      </header>
    </div>
  );
}

export default App;
