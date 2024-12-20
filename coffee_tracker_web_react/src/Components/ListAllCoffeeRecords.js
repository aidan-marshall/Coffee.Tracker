import {useEffect, useRef, useState} from "react";
import CoffeeTrackerClient from "../api/CoffeeTrackerClient";
import {HubConnectionBuilder} from "@microsoft/signalr";
import {useWebSocket} from "../WebSocketContext";

export function DisplayAllCoffeeRecords() {
    const [coffeeRecords, setCoffeeRecords] = useState([]);
    const [displayedRecords, setDisplayedRecords] = useState([]);
    const [filterDate, setFilterDate] = useState("");

    const webSocket = useWebSocket();

    const filteredCoffeeRecords = () => {
        return coffeeRecords.filter(record =>
            new Date(record.timeOfConsumption).toISOString().slice(0, 10) === filterDate);
    };

    const handleFilterChange = (e) => {
        const selectedDate = e.target.value;
        setFilterDate(selectedDate);

        if (selectedDate) {
            const filtered = filteredCoffeeRecords();
            setDisplayedRecords(filtered);
        } else {
            setDisplayedRecords(coffeeRecords);
        }
    };

    useEffect(() => {

        CoffeeTrackerClient.getAll().then(records =>
        {
            setCoffeeRecords(records);
            setDisplayedRecords(records);
        });

        if (webSocket) {
            webSocket.on("RecordAdded", (updatedRecords) => {
            console.log("New record added: updating records...", updatedRecords);
            setCoffeeRecords(updatedRecords);
            setDisplayedRecords(updatedRecords);
      });
    }

        return () => {
            if (webSocket) {
                webSocket.off("RecordAdded");
            }
            }
    }, [webSocket, coffeeRecords]);



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

                {displayedRecords && displayedRecords.length > 0 ? (
                    <ul>
                        {displayedRecords.map(coffeeRecord => (
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