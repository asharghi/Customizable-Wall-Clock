<template>
  <div class="container">
    <div class="empty-settings-row"></div>
    <BoolSetting
      label="Aktiver"
      :modelValue="localSettings.active"
      @change="updateValue('active', $event)"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Størrelse"
      :min="0"
      :max="100"
      :modelValue="localSettings.size"
      :disabled="!localSettings.active"
      @change="updateValue('size', parseInt($event))"
    />
     <SliderSetting
      label="Luft fra kanten"
      :min="0"
      :max="50"
      :modelValue="localSettings.space"
      :disabled="!localSettings.active"
      @change="updateValue('space', parseInt($event))"
    />
    <div class="empty-settings-row"></div>
    <SliderSetting
      label="Farge"
      :min="0"
      :max="360"
      :modelValue="localSettings.hue"
      :disabled="!localSettings.active"
      @change="updateValue('hue', parseInt($event))"
      sliderClass="color-picker"
    />
    <SliderSetting
      label="Lysstyrke"
      :min="0"
      :max="100"
      :modelValue="localSettings.luminosity"
      :disabled="!localSettings.active"
      @change="updateValue('luminosity', parseInt($event))"
      sliderClass="darkness-picker"
    />
    <div class="empty-settings-row"></div>
    <FontFaceSetting @selected="updateValue('fontface', $event)"/>
  </div>
</template>

<script>
import SliderSetting from "./SliderSetting";
import BoolSetting from "./BoolSetting";
import FontFaceSetting from "./FontFaceSetting";
export default {
  components: {
    SliderSetting,
    BoolSetting,
    FontFaceSetting
  },
  props: {
    settings: {
      type: Object,
      default: () => {
        return {
          width: 5,
          height: 5,
          radius: 5,
          hue: 50,
          luminosity: 50,
          active: false
        };
      }
    }
  },
  data() {
    return {
      localSettings: this.settings
    };
  },
  methods: {
    updateValue(dataPropertyName, value) {
      this.localSettings[dataPropertyName] = value;
      this.$emit("change", this.localSettings);
    }
  },
  watch: {
    settings: {
      handler(newValue) {
        this.localSettings = newValue;
      },
      deep: true
    }
  }
};
</script>
<style scoped>
.container {
  display: inline-block;
  width: 100%;
  position: absolute;
}
</style>