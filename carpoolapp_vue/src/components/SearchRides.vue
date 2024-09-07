<template>
    <v-sheet
        :elevation="7"
        border
        rounded="xl"
        class="pa-4 mx-4 mb-4"
    >
      <v-row>
        <v-col class="d-flex">
          <v-autocomplete
              chips
              clearable
              label="From"
              v-model="filters.from"
              :items=locationOptions
              variant="underlined"
          />
          <v-btn
              v-if="filters.to || filters.from"
              variant="text"
              color="primary"
              icon="mdi-swap-horizontal"
              class="ml-5 my-2"
              @click="swap()"
          />
        </v-col>
        <v-divider vertical class="border-opacity-50 rounded"/>
        <v-col>
          <v-autocomplete
              chips
              clearable
              label="To"
              v-model="filters.to"
              :items=locationOptions
              variant="underlined"
          />
        </v-col>
        <v-divider vertical class="border-opacity-50 rounded"/>
        <v-col>
          <v-date-input
              clearable
              label="Departure date"
              variant="underlined"
              v-model="filters.date"
          />
        </v-col>
        <v-divider vertical class="border-opacity-50 rounded"/>
        <v-col>
          <v-text-field
              v-model="filters.seats"
              label="Seats"
              variant="underlined"
          />
        </v-col>
      </v-row>
    </v-sheet>
</template>

<script>
import {mapActions, mapState} from "pinia";
import {useLocationsStore} from "../store/utilsStore.js";

import {VDateInput} from 'vuetify/labs/VDateInput'
import {debounce} from "lodash";

export default {
  name: 'SearchBar',
  components: {
    VDateInput
  },
  data() {
    return {
      locationOptions: [],

      filters: {
        from: null,
        to: null,
        date: null,
        seats: null
      }
    };
  },

  computed: {
    ...mapState(useLocationsStore, ['locations'])
  },

  watch: {
    filters: {
      handler(){
        this.searchHandler();
      },
      deep: true
    }
  },

  async beforeMount() {
    await this.getLocationOptions();
    this.locationOptions = this.locations;
  },

  methods: {
    ...mapActions(useLocationsStore, ['getLocationOptions']),

    search() {
      this.$emit("filter-rides", this.filters);
    },

    swap() {
      [this.filters.from, this.filters.to] = [this.filters.to, this.filters.from];
    },

    searchHandler: debounce(function () {
      this.search();
    }, 250)

  }
}
</script>

<style scoped>

</style>