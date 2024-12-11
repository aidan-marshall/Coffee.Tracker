import {useEffect, useState} from "react";
import CoffeeTrackerClient from "../api/CoffeeTrackerClient";

export function DisplayAllCoffeeRecords() {
    const [coffeeRecords, setCoffeeRecords] = useState([]);

    useEffect(() => {
        CoffeeTrackerClient.getAll().then(data => {
            if (data) {
                setCoffeeRecords(data);
            } else {
                setCoffeeRecords([]); 
            }
        }).catch(err => {
            console.error("Error fetching coffee records:", err);
            setCoffeeRecords([]);
        });
    }, []);

    return (
        <div>
            {coffeeRecords && coffeeRecords.length > 0 ? (
                <ul>
                    {coffeeRecords.map(coffeeRecord => (
                        <li key={coffeeRecord.id}>
                            {coffeeRecord.location} - {coffeeRecord.coffeeType} at {coffeeRecord.timeOfConsumption}
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No coffee records found</p>
            )}
        </div>
    );
}