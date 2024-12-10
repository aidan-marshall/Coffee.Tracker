import {useEffect, useState} from "react";
import {GetAll} from "../CoffeeTrackerClient";

export function DisplayAllCoffeeRecords() {
    const [coffeeRecords, setCoffeeRecords] = useState([]); // Initialize as an empty array

    useEffect(() => {
        GetAll().then(data => {
            if (data) {
                setCoffeeRecords(data);  // Populate with data if it's available
            } else {
                setCoffeeRecords([]);    // Set to empty array if no data
            }
        }).catch(err => {
            console.error("Error fetching coffee records:", err);
            setCoffeeRecords([]);  // Handle errors gracefully by setting to empty array
        });
    }, []);

    // Add a check to ensure `coffeeRecords` is an array before calling `.map()`
    return (
        <div>
            <h1>All Coffee Records</h1>
            {coffeeRecords && coffeeRecords.length > 0 ? (
                <ul>
                    {coffeeRecords.map(coffeeRecord => (
                        <li key={coffeeRecord.id}>
                            {coffeeRecord.location} - {coffeeRecord.coffeeType} at {coffeeRecord.timeOfConsumption}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No coffee records found</p>  // Display a message if no records are available
            )}
        </div>
    );
}