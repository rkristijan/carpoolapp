import {defineStore} from 'pinia'
import {getRidesHelper} from "../services/api/rides.js";

export const useRidesStore = defineStore('Rides',
    {
        state: () => ({
            initialRides: [],
            initialPriceRange: {
                lower: null,
                upper: null
            },

            rides: [],

            sortBy: null,
            ascending: false,

            currentPriceRange: {
                lower: null,
                upper: null,
            }
        }),

        actions: {
            async getAllRides() {
                try {
                    const result = await getRidesHelper();
                    if (result) {
                        this.initialRides = result.data;
                        this.calculateInitialLowerUpper();

                        this.rides = [...result.data]; // shallow copy
                        await this.sortRides();
                    }
                } catch (e) {
                    console.log(e);
                }
            },

            calculateInitialLowerUpper() {
                if (this.initialRides.length) {
                    this.initialPriceRange.lower = Math.min(...this.initialRides.map(r => r.price));
                    this.initialPriceRange.upper = Math.max(...this.initialRides.map(r => r.price));
                }
            },

            async updateSortParams(sortBy, ascending) {
                this.sortBy = sortBy;
                this.ascending = ascending;

                await this.sortRides();
            },

            updatePriceRangeParams(lowerValue, upperValue) {
                this.currentPriceRange.lower = lowerValue;
                this.currentPriceRange.upper = upperValue;

                this.rangeFilter();
            },

            async sortRides() {
                if (!this.sortBy) {
                    this.rides = [...this.initialRides]; // shallow copy to reset
                    return;
                }

                this.rides.sort((a, b) => {
                    let valueA = a[this.sortBy];
                    let valueB = b[this.sortBy];

                    if (this.sortBy === 'departureDate' || this.sortBy === 'creationDate') {
                        valueA = new Date(valueA);
                        valueB = new Date(valueB);
                    }

                    return this.ascending ? valueA - valueB : valueB - valueA;
                });
            },

            rangeFilter() {
                this.rides = [...this.initialRides.filter(r => r.price >= this.currentPriceRange.lower && r.price <= this.currentPriceRange.upper)];
            }
        }
    });