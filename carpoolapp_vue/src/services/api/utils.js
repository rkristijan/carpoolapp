import axios from "axios";

export async function fetchLocationOptions() {
    return await axios.get('https://localhost:7125/api/locations');
}