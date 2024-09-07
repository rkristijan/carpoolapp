import {defineStore} from "pinia";

import {fetchLocationOptions} from "../services/api/utils.js";

export const useLocationsStore = defineStore('Locations', {
    state: () => ({
        locations: []
    }),

    actions: {
        async getLocationOptions() {
            if (this.locations.length){
                return this.locations;
            }
            const result = await fetchLocationOptions()
            this.locations = result.data;

            return this.locations;
        }
    }
});