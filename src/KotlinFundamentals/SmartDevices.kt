import kotlin.properties.ReadWriteProperty
import kotlin.reflect.KProperty

open class SmartDevice protected constructor (val name: String, val category: String) {
    
    var deviceStatus = "online"
        protected set

    open val deviceType = "unknown"
    
    constructor(name: String, category: String, statusCode: Int): this(name, category) {
        deviceStatus = when(statusCode) {
            0 -> "offline"
            1 -> "online"
            else -> "unknown"
        }
    }

    fun printDeviceInfo() {
        println("Device name: $name, category: $category, type: $deviceType, status: $deviceStatus.")
    }
    
    open fun turnOn() {
        deviceStatus = "on"
        println("$name is turned on.")
    }

    open fun turnOff() {
        deviceStatus = "off"
        println("$name is turned off.")
    }
}

class SmartTvDevice(deviceName: String, deviceCategory: String, deviceStatusCode: Int) :
	SmartDevice(name = deviceName, category = deviceCategory, statusCode = deviceStatusCode) {
        
        override val deviceType = "Smart TV"

        private var speakerVolume by RangeRegulator(initialValue = 2, minValue = 0, maxValue = 100)
        private var channelNumber by RangeRegulator(initialValue = 1, minValue = 0, maxValue = 200)
       
       override fun turnOn() {
            super.turnOn()
            println("Speaker volume is set to $speakerVolume and channel number is set to $channelNumber.")
        }
        
        fun increaseSpeakerVolume() {
           speakerVolume++
           println("Speaker volume increased to $speakerVolume.")
       }

       fun decreaseSpeakerVolume() {
            speakerVolume--
       }
       
       internal fun nextChannel() {
           channelNumber++
           println("Channel number increased to $channelNumber.")
       }

       fun previousChannel() {
            channelNumber--
       }
    }

class SmartLightDevice(deviceName: String, deviceCategory: String, deviceStatusCode: Int) :
    SmartDevice(name = deviceName, category = deviceCategory, statusCode = deviceStatusCode) {
        
        override val deviceType = "Smart Light"
        private var brightnessLevel by RangeRegulator(initialValue = 7, minValue = 0, maxValue = 100)
        
        fun increaseBrightnessLevel() {
            brightnessLevel++
            println("Brightness level increased to $brightnessLevel.")
        }

        fun decreaseBrightness() {
            brightnessLevel--
        }

        override fun turnOn() {
            super.turnOn()
            brightnessLevel = 2
            print("The brightness level is $brightnessLevel.")
        }

        override fun turnOff() {
            super.turnOff()
            brightnessLevel = 0
        }
    }

interface ISmartHome {
    val deviceTurnOnCount: Int
    fun turnOnTv()
    fun turnOffTv()
    fun increaseTvVolume()
    fun decreaseTvVolume()
    fun changeTvChannelToNext()
    fun changeTvChannelToPrevious()
    fun turnOnLight()
    fun turnOffLight()
    fun increaseLightBrightness()
    fun decreaseLightBrightness()
    fun turnOffAllDevices()
    fun printSmartTvInfo()
    fun printSmartLightInfo()
}

class SmartHome(
    val smartTvDevice: SmartTvDevice,
    val smartLightDevice: SmartLightDevice) : ISmartHome {

    override var deviceTurnOnCount = 0
        private set

    override fun turnOnTv() {
        deviceTurnOnCount++
        smartTvDevice.turnOn()
    }

    override fun turnOffTv() {
        deviceTurnOnCount--
        smartTvDevice.turnOff()
    }

    override fun increaseTvVolume() {
        smartTvDevice.increaseSpeakerVolume()
    }

    override fun decreaseTvVolume() {
        smartTvDevice.decreaseSpeakerVolume()
    }

    override fun changeTvChannelToNext() {
        smartTvDevice.nextChannel()
    }

    override fun changeTvChannelToPrevious() {
        smartTvDevice.previousChannel()
    }

    override fun turnOnLight() {
        deviceTurnOnCount++
        smartLightDevice.turnOn()
    }

    override fun turnOffLight() {
        deviceTurnOnCount--
        smartLightDevice.turnOff()
    }

    override fun increaseLightBrightness() {
        smartLightDevice.increaseBrightnessLevel()
    }

    override fun decreaseLightBrightness() {
        smartLightDevice.decreaseBrightness()
    }

    override fun turnOffAllDevices() {
        turnOffTv()
        turnOffLight()
    }

    override fun printSmartTvInfo() {
        smartTvDevice.printDeviceInfo()
    }

    override fun printSmartLightInfo() {
        smartLightDevice.printDeviceInfo()
    }
}

class GuardedSmartHome(smartHome: ISmartHome): ISmartHome by smartHome{
    override increaseTvVolume(){
        if(smartHome.deviceStatus == "online"){
            smartHome.increaseTvVolume()
        }
    }
}

class RangeRegulator(
    initialValue: Int,
    private val minValue: Int,
    private val maxValue: Int
) : ReadWriteProperty<Any?, Int> {
    
    var fieldData = initialValue

    override fun getValue(thisRef: Any?, property: KProperty<*>): Int {
        return fieldData
    }

    override fun setValue(thisRef: Any?, property: KProperty<*>, value: Int) {
        if (value in minValue..maxValue) {
            fieldData = value
        }
    }
}

fun main() {
    val smartHome: ISmartHome = SmartHome(
        SmartTvDevice(deviceName = "Android TV", deviceCategory = "Entertainment", 1),
        SmartLightDevice("Google Light", "Utility", 0)
    )

    //val guardedSmartHome: ISmartHome = GuardedSmartHome(smartHome)

    smartHome.turnOnTv()
    smartHome.turnOnLight()
    println("Total number of devices currently turned on: ${smartHome.deviceTurnOnCount}")
    println()
    
    //guardedSmartHome.increaseTvVolume()
    smartHome.changeTvChannelToNext()
    smartHome.increaseLightBrightness()
    println()

    smartHome.decreaseTvVolume()
    smartHome.decreaseLightBrightness()
    smartHome.changeTvChannelToPrevious()
    smartHome.printSmartTvInfo()
    smartHome.printSmartLightInfo()

    smartHome.turnOffAllDevices()
    println("Total number of devices currently turned on: ${smartHome.deviceTurnOnCount}")
}