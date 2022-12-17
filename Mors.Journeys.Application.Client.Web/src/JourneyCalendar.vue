<script setup>
import { computed, reactive, ref } from 'vue'
import Calendar from './Calendar.vue'
import * as api from './api'

const props = defineProps({ passenger: Object });
const journeys = ref({});

api.journeys().then(x => journeys.value = x);

const monthYear = reactive({
    year: 2014,
    month: 1,
    next: function () {
        const newMonth = this.month == 12 ? 1 : this.month + 1;
        this.month = newMonth;
        if (newMonth == 1) {
            this.year += 1;
        }
    },
    previous: function () {
        const newMonth = this.month == 1 ? 12 : this.month - 1;
        this.month = newMonth;
        if (newMonth == 12) {
            this.year -= 1;
        }
    }
})

const journeysOfPassenger = computed(() => {
    return journeys.value?.[props.passenger.id] ?? {};
});
</script>

<template>
    <div>
        <h2>{{ props.passenger.name }}</h2>
        <Calendar :month="monthYear.month" :year="monthYear.year">
            <template #header="header">
                <div class="d-flex justify-content-center align-items-baseline">
                    <button type="button" class="btn" @click="monthYear.previous()">&lt;</button>
                    <div class="d-flex justify-content-center" style="min-width: 15ch;">
                        {{ `${header.monthName} ${header.year}` }}
                    </div>
                    <button type="button" class="btn " @click="monthYear.next()">&gt;</button>
                </div>
            </template>
            <template #item="cell">
                <template v-if="!cell.outside">
                    <div>{{ cell.day }}</div>
                    <template v-if="journeysOfPassenger?.[cell.year]?.[cell.month]?.[cell.day]">
                        <div>{{ journeysOfPassenger?.[cell.year]?.[cell.month]?.[cell.day]?.liftCount }} / {{
                                journeysOfPassenger?.[cell.year]?.[cell.month]?.[cell.day]?.liftDistance
                        }}</div>
                        <div>{{ journeysOfPassenger?.[cell.year]?.[cell.month]?.[cell.day]?.journeyCount }} / {{
                                journeysOfPassenger?.[cell.year]?.[cell.month]?.[cell.day]?.journeyDistance
                        }}</div>
                    </template>
                </template>
            </template>
        </Calendar>
    </div>
</template>

<style scoped>

</style>