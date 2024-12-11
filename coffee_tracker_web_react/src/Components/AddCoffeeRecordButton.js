import {AddCoffeeRecordForm} from "../forms/CoffeeRecordForm";
import {Link, useNavigate} from "react-router-dom";

export function AddCoffeeRecord() {
    const navigate = useNavigate();

    function handleClick() {
        navigate("/add");
    }
    
    return (
        <button onClick={handleClick}>
            Add Coffee Record
        </button>
    );
}