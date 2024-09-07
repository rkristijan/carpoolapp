<template>
  <div class="pa-4 text-center">
    <v-dialog
        v-model="dialog"
        max-width="600"
    >
      <template v-slot:activator="{ props: activatorProps }">
        <v-btn
            class="my-2"
            prepend-icon="mdi-plus-circle-outline"
            variant="text"
            color="primary"
            size="large"
            v-bind="activatorProps"
        >
          Publish a ride
        </v-btn>
      </template>

      <v-form
        ref="form"
        v-model="valid"
        @submit.prevent="handleSubmit"
      >
        <v-card
            prepend-icon="mdi-car-hatchback"
            title="Publish a ride"
        >
          <v-card-text>
            <v-row dense>
              <v-col
                  cols="12"
                  md="4"
                  sm="6"
              >
                <v-text-field
                    label="Driver name"
                    v-model="form.driverName"
                    :rules="[rules.required]"
                />
              </v-col>

              <v-col
                  cols="12"
                  md="4"
                  sm="6"
              >
                <v-date-input
                    clearable
                    :allowed-dates="allowedDates"
                    label="Departure date"
                    prepend-icon=""
                    prepend-inner-icon="$calendar"
                    v-model="form.departureDate"
                    :rules="[rules.required]"
                />
              </v-col>

              <v-col
                  cols="12"
                  md="4"
                  sm="6"
              >
                <v-text-field
                    label="Available seats"
                    v-model="form.availableSeats"
                    :rules="[rules.required, rules.isPositiveInteger]"
                />
              </v-col>
              <v-col
                  cols="12"
                  md="4"
                  sm="6"
              >
                <v-text-field
                    label="Vehicle seats"
                    v-model="form.vehicleSeats"
                    :rules="[rules.required, rules.isPositiveInteger]"
                />
              </v-col>
              <v-col
                  cols="12"
                  md="4"
                  sm="6"
              >
                <v-text-field
                    label="Price"
                    v-model="form.price"
                    :rules="[rules.required, rules.isPositiveInteger]"
                />
              </v-col>
            </v-row>
            <v-row dense>
              <v-col
                  cols="12"
                  sm="6"
              >
                <v-autocomplete
                    :items=locationOptions
                    label="From"
                    :rules="[rules.required]"
                    v-model="form.startLocation"
                />
              </v-col>

              <v-col
                  cols="12"
                  sm="6"
              >
                <v-autocomplete
                    :items=locationOptions
                    label="To"
                    :rules="[rules.required]"
                    v-model="form.endLocation"
                />
              </v-col>

            </v-row>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions class="d-flex justify-space-between">
            <v-btn
                text="Close"
                variant="plain"
                @click="handleClose"
            />

            <v-btn
                color="primary"
                text="Save"
                variant="tonal"
                type="submit"
            />

          </v-card-actions>
        </v-card>
      </v-form>
    </v-dialog>
  </div>
</template>

<script>
import {isInteger} from "lodash";
import { VDateInput } from 'vuetify/labs/VDateInput'
import {mapActions, mapState} from "pinia";
import {useLocationsStore} from "../../store/utilsStore.js";
import {createRideHelper} from "../../services/api/rides.js";

export default {
  components: {VDateInput},
  data(){
    return {
      dialog: false,
      valid: false,
      form: {
        driverName: null,
        departureDate: null,
        availableSeats: null,
        price: null,
        startLocation: null,
        endLocation: null,
      },

      locationOptions: [],

      rules: {
        required: r => !!r || 'Field is required',
        isPositiveInteger: r => r > 0 && isInteger(parseInt(r)) || 'Value must be a positive integer',
      },
    }
  },

  computed: {
    ...mapState(useLocationsStore, ['locations'])
  },

  watch: {
    dialog() {
      if (!this.dialog) {
        this.handleClose();
      }
    }
  },

  async beforeMount() {
    await this.getLocationOptions();
    this.locationOptions = this.locations;
  },

  methods: {
    ...mapActions(useLocationsStore, ['getLocationOptions']),
    allowedDates(val) {
      return val >= new Date().setDate(new Date().getDate() - 1);
    },

    handleClose() {
      this.dialog = false;

      this.$refs.form.reset();
      this.form.departureDate = null;
    },

    async handleSubmit() {
      if (this.valid) {
        try {
          const result = await createRideHelper({
            driverName: this.form.driverName,
            startLocation: this.form.startLocation,
            endLocation: this.form.endLocation,
            availableSeats: this.form.availableSeats,
            vehicleSeats: this.form.vehicleSeats,
            price: this.form.price,
            departureDate: this.form.departureDate,
          });

          if (result.data){
            this.handleClose();
            this.$emit('handle-refetch')
          }
        } catch (error) {
          console.log(error);
        }
      }
    },
  }
}
</script>