import { useState } from "react";
import PassengersCounterDropdown from './PassengersCounterDropdown.jsx';
import '../Css/SearchBar.css';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { searchFlights } from "/api/FlightsSearchApi.js";
import { format } from 'date-fns';

const SearchBar = ({ onSearch }) => {
    const [formData, setFormData] = useState({
        origin: "",
        destination: "",
        departureDate: null,
        returnDate: null,
        adultsPassengers: 1,
        kidsPassengers: 0,
        currency: "",
    });

    const [tripType, setTripType] = useState("round-trip");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async () => {
        const requiredFields = ["origin", "destination", "departureDate", "currency"];
        const missingFields = requiredFields.filter(field => !formData[field]);

        if (missingFields.length > 0) {
            alert(`Please fill in all required fields: ${missingFields.join(", ")}`);
            return;
        }

        try {
            setLoading(true);

            const requestData = {
                ...formData,
                departureDate: format(formData.departureDate, 'yyyy-MM-dd'),
                ...(tripType === "round-trip" && formData.returnDate && {
                    returnDate: format(formData.returnDate, 'yyyy-MM-dd'),
                }),
                ...(tripType === "one-way" && { returnDate: null }),
            };

            const data = await searchFlights(requestData);
            onSearch(data, tripType);
        } catch (error) {
            alert("Error during search: " + error.message);
        } finally {
            setLoading(false);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (["origin", "destination"].includes(name) && !/^[A-Za-z]{0,3}$/.test(value)) return;

        setFormData(prevState => ({
            ...prevState,
            [name]: name === "origin" || name === "destination" ? value.toUpperCase() : value,
        }));
    };

    const handleDateChange = (date, field) => {
        setFormData(prevState => ({
            ...prevState,
            [field]: date,
        }));
    };

    const handlePassengersChange = (adults, kids) => {
        setFormData(prevState => ({
            ...prevState,
            adultsPassengers: adults,
            kidsPassengers: kids,
        }));
    };

    const handleTripTypeChange = (e) => {
        setTripType(e.target.value);
    };

    return (
        <div className="search-bar">
            <h1>Search Low-Cost Flights</h1>
            <div className="search-box">
                <div className="input-group">
                    <label>Trip Type</label>
                    <select value={tripType} onChange={handleTripTypeChange}>
                        <option value="one-way">One-Way</option>
                        <option value="round-trip">Round-Trip</option>
                    </select>
                </div>

                <div className="input-group">
                    <label>From</label>
                    <input
                        type="text"
                        name="origin"
                        placeholder="ZAG"
                        value={formData.origin}
                        onChange={handleChange}
                        maxLength={3}
                    />
                </div>

                <div className="input-group">
                    <label>To</label>
                    <input
                        type="text"
                        name="destination"
                        placeholder="BOS"
                        value={formData.destination}
                        onChange={handleChange}
                        maxLength={3}
                    />
                </div>

                <div className="input-group">
                    <label>Depart</label>
                    <DatePicker
                        selected={formData.departureDate}
                        onChange={(date) => handleDateChange(date, 'departureDate')}
                        dateFormat="dd.MM.yyyy"
                        className="form-control"
                        placeholderText="Select departure date"
                    />
                </div>

                {tripType === "round-trip" && (
                    <div className="input-group">
                        <label>Return</label>
                        <DatePicker
                            selected={formData.returnDate}
                            onChange={(date) => handleDateChange(date, 'returnDate')}
                            dateFormat="dd.MM.yyyy"
                            className="form-control"
                            placeholderText="Select return date"
                        />
                    </div>
                )}

                <div className="input-group passengers-group">
                    <label>Passengers</label>
                    <PassengersCounterDropdown
                        adults={formData.adultsPassengers}
                        kids={formData.kidsPassengers}
                        onChange={handlePassengersChange}
                    />
                </div>

                <div className="input-group">
                    <label>Currency</label>
                    <select
                        name="currency"
                        value={formData.currency}
                        onChange={handleChange}
                    >
                        <option value="">Select Currency</option>
                        <option value="USD">USD</option>
                        <option value="EUR">EUR</option>
                        <option value="HRK">HRK</option>
                    </select>
                </div>

                <button className="search-button" onClick={handleSubmit}>
                    {loading ? "Loading..." : "Search"}
                </button>
            </div>

            {loading && (
                <div className="loader-overlay">
                    <div className="loader"></div>
                </div>
            )}
        </div>
    );
};

export default SearchBar;
