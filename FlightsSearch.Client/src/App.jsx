import { useState } from 'react';
import FlightResultsTable from './components/FlightResultsTable';
import SearchBar from './Components/SearchBar';

function App() {

    const [searchResults, setSearchResults] = useState([]);
    const [tripType, setTripType] = useState("round-trip");

    const handleSearchResults = (results, tripType) => {
        setSearchResults(results);
        setTripType(tripType);
    };

    return (
        <div>
            <SearchBar onSearch={handleSearchResults} />
            <FlightResultsTable results={searchResults} isOneWay={tripType === "one-way"} />
        </div>
    );
}

export default App;
