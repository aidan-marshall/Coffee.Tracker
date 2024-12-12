import {useEffect, useState} from "react";
import CoffeeTrackerClient from "../api/CoffeeTrackerClient";

export function DisplayAllCoffeeRecords() {
    const [coffeeRecords, setCoffeeRecords] = useState([]);
    const [filteredRecords, setFilteredRecords] = useState([]);
    const [filterDate, setFilterDate] = useState("");

    useEffect(() => {
        CoffeeTrackerClient.getAll()
            .then(data => {
            if (data) {
                setCoffeeRecords(data);
                setFilteredRecords(data);
            }
        }).catch(err => {
            console.error("Error fetching coffee records:", err);
            setCoffeeRecords([]);
            setFilteredRecords([]);
        });
    }, []);
    
    const handleFilterChange = (e) => {
        const selectedDate = e.target.value;
        setFilterDate(selectedDate);
        
        if (selectedDate) {
            const filtered = coffeeRecords.filter(record => 
            new Date(record.timeOfConsumption).toISOString().slice(0, 10) === selectedDate
            );
            setFilteredRecords(filtered);
        }
        else {
            setFilteredRecords(coffeeRecords);
        }
    };

    return (

        <div>
            <div>
                <label htmlFor="filter-date">Filter by Date:</label>
                <input
                    type="date"
                    id="filter-date"
                    value={filterDate}
                    onChange={handleFilterChange}
                />
            </div>
            
            {filteredRecords && filteredRecords.length > 0 ? (
                <ul>
                    {filteredRecords.map(coffeeRecord => (
                        <li key={coffeeRecord.id}>
                            {coffeeRecord.location} - {coffeeRecord.coffeeType} at {new Date(coffeeRecord.timeOfConsumption).toLocaleString()}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No coffee records found</p>
            )}
        </div>
    );
}