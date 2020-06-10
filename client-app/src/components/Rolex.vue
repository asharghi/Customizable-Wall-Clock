<template>
  <div>
    <div class="clockface center" :style="clockStyle">
      <span class="date"></span>
      <div class="glass"></div>
      <img class="hours arrow center" src="https://i.imgur.com/rvzsdlL.png" />
      <img class="minutes arrow center" src="https://i.imgur.com/uNO8uLb.png" />
      <img class="seconds arrow center" src="https://i.imgur.com/wWq8zSO.png" />
    </div>
    <link href="https://fonts.googleapis.com/css?family=Varela+Round&display=swap" rel="stylesheet" />
  </div>
</template>

<script>
export default {
  data() {
    return {
      rotateIntervalId: "",
      timerIntervalId: ""
    };
  },
  props: {
    watchFaceSettings: Object
  },
  computed: {
    clockStyle() {
      return `
        transform: scale(0.${this.watchFaceSettings.size});
        transform-origin: 0 0;
        left: ${this.watchFaceSettings.left}px;
        top: ${this.watchFaceSettings.top}px;
        position: absolute;
      `;
    }
  },
  mounted() {
    this.startClock();
  },
  beforeDestroy() {
    clearInterval(this.rotateIntervalId);
    clearInterval(this.timerIntervalId);
  },
  methods: {
    startClock() {
      let oneTenthSeconds = new Date().getSeconds() * 10;
      let minutes = 0;
      let hours = 0;
      let day = 0;

      const secondsNode = document.querySelector(".seconds.arrow");
      const minutesNode = document.querySelector(".minutes.arrow");
      const hoursNode = document.querySelector(".hours.arrow");
      const dateNode = document.querySelector(".date");

      const timeToDegree = time => {
        return (time / 60) * 360;
      };
      const rotateArrow = (node, degree) => {
        node.style.transform =
          "translate(-50%, -50%) rotate(" + degree + "deg)";
      };
      this.rotateIntervalId = setInterval(() => {
        rotateArrow(secondsNode, timeToDegree(oneTenthSeconds / 10));
        oneTenthSeconds++;
      }, 100);

      this.timerIntervalId = setInterval(() => {
        const now = new Date();

        oneTenthSeconds = now.getSeconds() * 10;

        const currentMinutes = now.getMinutes();
        if (currentMinutes != minutes) {
          minutes = currentMinutes;
          rotateArrow(minutesNode, timeToDegree(minutes));
        }
        const currentHour = now.getHours();
        if (currentHour != hours) {
          hours = currentHour;
          rotateArrow(hoursNode, timeToDegree(hours));
        }
        const currentDay = now.getDate();
        if (currentDay != day) {
          day = currentDay;
          dateNode.innerHTML = day;
        }
      }, 1000);
    }
  }
};
</script>

<style scoped>
.clockface {
  width: 714px;
  height: 714px;
  background: gray;
  border-radius: 50%;
  background-image: url("https://i.imgur.com/bmFViar.png");
}
.arrow {
  width: 55px;
  height: 100%;
}
.center {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
.date {
  position: absolute;
  top: 337px;
  right: 167px;
  color: #303032;
  font-size: 48px;
  font-family: "Varela Round";
}
.glass {
  opacity: 0.4;
  border-radius: 50px;
  position: absolute;
  top: 365px;
  right: 144px;
  width: 100px;
  height: 30px;
  background: white;
}
body {
  background-color: black;
}
</style>
