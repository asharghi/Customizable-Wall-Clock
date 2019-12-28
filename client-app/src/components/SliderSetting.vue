<template>
  <div :class="{'slider-row' : true, 'disabled': disabled}">
    <div class="settings-font">
      <span>{{label}}</span>
      <span v-if="!hideNumberValue" class="slider-number-value">{{modelValue}}</span>
    </div>
    <input
      :disabled="disabled"
      @input="$emit('change', localValue)"
      type="range"
      :min="min"
      :step="step"
      :max="max"
      v-model="localValue"
      :class="'range-slider ' + sliderClass"
    />
  </div>
</template>

<script>
export default {
  props: {
    max: {
      type: Number,
      default: 25
    },
    min: {
      type: Number,
      default: 0
    },
    step: {
      type: Number,
      default: 1
    },
    modelValue: {
      type: Number,
      default: 0
    },
    label: {
      type: String,
      default: ""
    },
    sliderClass: {
      type: String,
      default: ""
    },
    hideNumberValue: {
      type: Boolean,
      default: false
    },
    disabled: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      localValue: this.modelValue
    };
  },
  watch: {
    modelValue: function(newValue, oldValue) {
      this.localValue = newValue;
    }
  }
};
</script>
<style scoped>
.range-slider {
  appearance: none;
  outline: none;
  width: 100%;
  height: 3px;
  border-radius: 20px;
  margin-top: 5px;
  background-color: #d8d8d8;
}
.range-slider::-webkit-slider-thumb {
  background: white;
  appearance: none;
  box-shadow: 1px 2px 26px 1px #bdbdbd;
  width: 20px;
  height: 20px;
  border: 5px solid #fafafa;
  border-radius: 50%;
}
.range-slider.color-picker {
  height: auto;
  background: linear-gradient(
    to right,
    #ff0000,
    #ffd500,
    #55ff00,
    #00ff80,
    #00aaff,
    #2b00ff,
    #ff00ff,
    #ff002b,
    black
  );
}
.range-slider.darkness-picker {
  height: auto;
  background: linear-gradient(to right, black, white);
}
.slider-row {
  padding: 10px 20px 10px 20px;
  background: #ffffff;
  border-bottom: 1px solid #d8d8d8;
}
.slider-row.disabled{
  opacity: 0.5;
}
.slider-number-value {
  float: right;
}
</style>