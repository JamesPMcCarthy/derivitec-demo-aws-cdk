<template>
  <div :class="[{'timer':true},{'alarm':soundAlarm}]">

    <v-time-picker
      format="24hr"
      v-model="selectedTime"
      use-seconds
      @input="BackupTime()"
      ref="timepicker"
      :color="clockColor"
    ></v-time-picker>

    <div >

      <div class="control-btns">
        <v-btn
          elevation="2"      
          :color="pollingEvent === -1 ? 'success' : 'error'"
          @click="triggerTimer()"
          :disabled="selectedTime === '00:00:00'">{{(pollingEvent === -1 ? "Start" : "Stop") + " Timer"}}</v-btn>
      </div>

      <div>
        <v-btn
          class="control-btns"
          elevation="2"      
          color="warning"
          @click="RevertBackupTime()"
          :disabled="selectedTimeCpy === '00:00:00' || selectedTimeCpy === selectedTime ">reset</v-btn>
        <v-btn
          class="control-btns"
          elevation="2"      
          color="error"
          @click="ClearSelection()"
          :disabled="selectedTime === '00:00:00'">Clear</v-btn>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Timer extends Vue {

  $refs!: {
    timepicker: HTMLFormElement
  }

  private selectedTime = "00:00:30";
  private selectedTimeCpy = "00:00:30";
  private pollingEvent?: number = -1;
  private pollingEventColour?: number = -1;
  private clockColor = "primary";
  private soundAlarm = false;

  public toggleflashColourFinish() : void {
    if(this.pollingEventColour === -1){
       this.pollingEventColour = setInterval(()=>{this.toggleColour()}, 500);
    }
    else{
       clearInterval(this.pollingEventColour)
       this.pollingEventColour = -1;
    }
  }

   public toggleColour(): void {      
      if(this.clockColor == "primary")
      {
         this.clockColor = "error";
      }
      else{
          this.clockColor = "primary";
      }
   }

  public triggerTimer(): void {    

    try{

      if(this.selectedTime === "00:00:00")
      {
        throw "No Time Set";
      }

      if(this.pollingEvent === -1){
        this.$refs.timepicker.selecting = 3;
        this.pollingEvent = setInterval(()=>{this.timerEvent()}, 1000);
      }
      else{
         this.CancelEvent();
      }
    }
    catch(e){
      console.log(e);
    }    
  }
  
  public BackupTime() : void{
    this.CancelEvent();
    this.selectedTimeCpy = JSON.parse(JSON.stringify(this.selectedTime));
  }

  public RevertBackupTime() : void{
    this.selectedTime = JSON.parse(JSON.stringify(this.selectedTimeCpy));
    this.CancelEvent();
  }

  public ClearSelection() : void{
    this.selectedTime = "00:00:00";
    this.selectedTimeCpy = "00:00:00";
    this.CancelEvent();
  }

  public CancelEvent(): void {    
    if(this.pollingEvent !== -1)
    {
      clearInterval(this.pollingEvent);
      this.pollingEvent = -1;
    }
  }

  public timerEvent(): void{
    let selectedDateTime = new Date('1970-01-01T' + this.selectedTime);
    selectedDateTime.setTime(selectedDateTime.getTime() - 1000);
    
    this.selectedTime = `${selectedDateTime.getHours()<10 ? '0' + selectedDateTime.getHours() : selectedDateTime.getHours()}:` +
                        `${selectedDateTime.getMinutes()<10 ? '0' + selectedDateTime.getMinutes() : selectedDateTime.getMinutes()}:` +
                        `${selectedDateTime.getSeconds()<10 ? '0' + selectedDateTime.getSeconds() : selectedDateTime.getSeconds()}`;  

    if(this.selectedTime === "00:00:00")
    {
      this.soundAlarm = true;
      this.toggleflashColourFinish();
      setTimeout(()=>{
         this.soundAlarm = false; 
         this.toggleflashColourFinish();
         this.clockColor = "primary";
      },5000);
      this.CancelEvent();
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.timer{
  display:block;
  padding-top:50px;
}
.control-btns{
  margin:10px;
}
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.alarm {
  animation: shake 0.82s cubic-bezier(.36,.07,.19,.97) both infinite;
  transform: translate3d(0, 0, 0);
  backface-visibility: hidden;
  perspective: 1000px;
}

@keyframes shake {
  10%, 90% {
    transform: translate3d(-1px, 0, 0);
  }
  
  20%, 80% {
    transform: translate3d(2px, 0, 0);
  }

  30%, 50%, 70% {
    transform: translate3d(-4px, 0, 0);
  }

  40%, 60% {
    transform: translate3d(4px, 0, 0);
  }
}

</style>
