import { useState, useRef, useEffect } from "react";
import "../Css/PassengersCounterDropdown.css";

const PassengersCounterDropdown = ({ onChange }) => {
    const [adults, setAdults] = useState(1);
    const [children, setChildren] = useState(0);
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const dropdownRef = useRef(null);

    const toggleDropdown = () => {
        setIsDropdownOpen(!isDropdownOpen);
    };

    const increment = (setter, value, type) => {
        const newValue = value + 1;
        setter(newValue);
        emitChange(type, newValue);
    };

    const decrement = (setter, value, type) => {
        const newValue = value > 0 ? value - 1 : 0;
        setter(newValue);
        emitChange(type, newValue);
    };

    const emitChange = (type, value) => {
        const updatedValues = {
            adults: type === "adults" ? value : adults,
            children: type === "children" ? value : children,
        };
        onChange(updatedValues.adults, updatedValues.children);
    };

    useEffect(() => {
        const handleOutsideClick = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setIsDropdownOpen(false);
            }
        };

        document.addEventListener("click", handleOutsideClick);
        return () => {
            document.removeEventListener("click", handleOutsideClick);
        };
    }, []);

    return (
        <div className="counter-dropdown" ref={dropdownRef}>
            <div className="dropdown-header" onClick={toggleDropdown}>
                {adults} Adults, {children} Children
            </div>
            {isDropdownOpen && (
                <div className="dropdown-content">
                    <div className="counter-row">
                        <span>Adults</span>
                        <div className="counter-controls">
                            <button
                                onClick={() => decrement(setAdults, adults, "adults")}
                                disabled={adults <= 1}
                            >
                                -
                            </button>
                            <span>{adults}</span>
                            <button onClick={() => increment(setAdults, adults, "adults")}>+</button>
                        </div>
                    </div>
                    <div className="counter-row">
                        <span>Children</span>
                        <div className="counter-controls">
                            <button
                                onClick={() => decrement(setChildren, children, "children")}
                                disabled={children <= 0}
                            >
                                -
                            </button>
                            <span>{children}</span>
                            <button onClick={() => increment(setChildren, children, "children")}>+</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default PassengersCounterDropdown;
