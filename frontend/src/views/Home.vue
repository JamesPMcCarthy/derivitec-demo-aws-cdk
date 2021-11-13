<template>
  <div class="home">
    <Timer/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Timer from '@/components/Timer.vue'; // @ is an alias to /src

@Component({
  components: {
    Timer,
  },
})
export default class Home extends Vue {}
</script>
