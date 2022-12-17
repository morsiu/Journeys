<script setup>
import * as api from './api'
import { ref } from 'vue'

defineEmits(['passengerSelected']);
const passengers = ref([]);

api.passengers().then(x => passengers.value = x);
</script>

<template>
    <div>
        <h2>Passengers</h2>
        <div class="btn-group-vertical">
            <button type="button" class="btn btn-link" v-for="passenger in passengers"
                @click="$emit('passengerSelected', passenger)">
                {{ passenger.name }}
            </button>
        </div>
    </div>
</template>

<style scoped>

</style>
